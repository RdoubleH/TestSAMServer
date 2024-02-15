using LotusAPI;
using LotusAPI.Controls;
using LotusAPI.Data;
using LotusAPI.Math;
using LotusAPI.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Img = LotusAPI.MV.Image;

namespace TestSAMServer {
    public partial class Form1 : Form {
        SimpleTcpClient _client = new SimpleTcpClient();
        public Form1() {
            InitializeComponent();
            LotusAPI.Registry.SetApplicationName("ATMC\\Ex\\TestSAMServer");
            Logger.Add(new LogViewLogger(logView1));
            Library.Initialize();
        }

        void Connect() {
            _client.Disconnect();
            _client = new SimpleTcpClient();
            _client.Connect(tb_IP.Text, 8727, 10000, 10000);
            if (_client.Connected)
                Logger.Info("CONNECTED");
            else
                throw new Exception("Failed to connect!");
        }
        void Disconnect() {
            _client?.Disconnect();
        }

        LotusAPI.MV.Image _img;

        String GetJsonStr(Json j) => j.ToString().Replace("\n", "").Replace(" ", "") + "\r\n";
        void SendRequest(Json request) {
            if (!_client.Connected) throw new Exception("Client not connected!");
            var str = GetJsonStr(request);
            var buf = ASCIIEncoding.ASCII.GetBytes(str);
            _client.Send(buf, 0, buf.Length);
            var res = _client.ReadLine(1000);//expect OK 
            if (res != "OK") {
                throw new Exception($"Failed to send request (RES={res})");
            }
            Logger.Debug(">" + res);
        }

        void SetImage(Img img) {
            //convert image to PNG buffer
            byte[] imgbuf = img.EncodePNG();
            Json request = new Json();
            request["name"] = "set_img";
            request["size"] = imgbuf.Length;
            SendRequest(request);
            _client.Send(imgbuf, 0, imgbuf.Length);
            var res = _client.ReadLine(100000);//expect OK 
            if (res != "OK") {
                throw new Exception($"Failed to send request (RES={res})");
            }
            Logger.Debug(">" + res);
        }

        private void bt_LOAD_Click(object sender, EventArgs e) {
            try {
                _img = DialogUtils.OpenImage("Open image").ToBGR();
                iv.SetImage(_img);
                Connect();
                SetImage(_img);
                iv.AutoScale();
            }
            catch (Exception ex) { Logger.Error(ex.Message); Logger.Trace(ex.StackTrace); }
            Disconnect();
        }

        List<Point2i> _pnts = new List<Point2i>();
        private void iv_ROISelectedEvent(RectangleF roi) {
            if(ckb_InROI.Checked) Predict(roi, _pnts);
        }
        private void Predict(RectangleF roi, List<Point2i> pnts) {
            try {
                if (_img == null) throw new Exception("No image");
                //create request message
                Json request = new Json();
                request["name"] = "predict";
                var rect = (Rect2i)roi;
                request["box"][0] = rect.X;
                request["box"][1] = rect.Y;
                request["box"][2] = rect.X + rect.Width;
                request["box"][3] = rect.Y + rect.Height;
                if (pnts == null) {
                    var cen = (rect.TopLeft + rect.BottomRight) / 2;
                    cen.Write(request["point"][0]);
                    request["label"][0] = 1;
                }
                else {
                    for (int i = 0; i < pnts.Count; i++) {
                        pnts[i].Write(request["point"][i]);
                        request["label"][i] = 1;
                    }
                }

                request["multimask"] = true;
                var str = request.ToString().Replace("\n", "").Replace(" ", "") + "\r\n";
                Logger.Debug(str);

                Connect();

                //send request
                var buf = ASCIIEncoding.ASCII.GetBytes(str);
                _client.Send(buf, 0, buf.Length);
                var res = _client.ReadLine(1000);//expect OK 
                if (res != "OK") {
                    throw new Exception($"Failed to send request (RES={res})");
                }
                Logger.Debug(">" + res);

                res = _client.ReadLine(100000);//expect OK 
                //parse response
                Json jres = Json.FromString(res);
                int mask_id = jres["mask"];
                float score = jres["score"];
                int mask_size = jres["size"];
                byte[] res_buf = new byte[mask_size];
                _client.Read(res_buf, 0, mask_size);
                res = _client.ReadLine(1000);//expect OK 
                if (res != "OK") {
                    throw new Exception($"Failed to receive result data (RES={res})");
                }
                Logger.Debug(">" + res);
                //read mask data
                var mask_img = LotusAPI.MV.Image.Decode(res_buf, LotusAPI.MV.ImageDecodeOption.Grayscale).ToGray();

                //create overlay
                var img = _img.Clone() as LotusAPI.MV.Image;
                var bgr = img.Split();
                //blend result
                img = LotusAPI.MV.Image.Merge(new LotusAPI.MV.Image[] { bgr[0], bgr[1] * 0.5 + mask_img * 0.5, bgr[2] });
                //draw outline
                mask_img.FindContours(20)
                    .ToList()
                    .ForEach(x => img.DrawPoly(x, Color.Magenta, 2, true));
                iv.SetImage(img);
            }
            catch (Exception ex) { Logger.Error(ex.Message); Logger.Trace(ex.StackTrace); }
            Disconnect();
        }



        private void iv_MouseClick(FastImageView sender, FastImageView.MouseEventArgs e) {
            if(ModifierKeys == Keys.Control) {
                _pnts.Add(e.Location);
                if(!ckb_InROI.Checked) Predict(new RectangleF(), _pnts);
                iv.Invalidate();
            }
        }

        private void iv_PostRenderDrawEvent(FastImageView c) {
            try { 
                for(int i = 0; i < _pnts.Count; i++) {
                    c.DrawCircle(_pnts[i], 3, Color.Lime, -1);
                }
            }catch { }
        }

        private void iv_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) _pnts.Clear();
            iv.Invalidate();
        }
    }
}
