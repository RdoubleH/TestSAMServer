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

namespace TestSAMServer {
    public partial class Form1 : Form {
        SimpleTcpClient _client = new SimpleTcpClient();
        public Form1() {
            InitializeComponent();
            LotusAPI.Registry.SetApplicationName("ATMC\\Ex\\TestSAMServer");
            Logger.Add(new LogViewLogger(logView1));
            Library.Initialize();
        }

        private void bt_CONNECT_Click(object sender, EventArgs e) {
            try {
                _client.Disconnect();
                _client = new SimpleTcpClient();
                _client.Connect(tb_IP.Text, 8727, 10000, 10000);
                if(_client.Connected)
                    Logger.Info("CONNECTED");
                else
                    throw new Exception("Failed to connect!");
            } catch(Exception ex) { Logger.Error(ex.Message); Logger.Trace(ex.StackTrace); }
        }

        LotusAPI.MV.Image _img;
        byte[] _imgbuf;
        private void bt_LOAD_Click(object sender, EventArgs e) {
            try {
                _img = DialogUtils.OpenImage("Open image").ToBGR();
                _imgbuf = _img.EncodePNG();
                iv.SetImage(_img);
                iv.AutoScale();
            } catch(Exception ex) { Logger.Error(ex.Message); Logger.Trace(ex.StackTrace); }
        }

        private void iv_ROISelectedEvent(RectangleF roi) {
            try {
                if(_img == null) throw new Exception("No image");
                //create request message
                Json j = new Json();
                j["name"] = "predict";
                j["size"] = _imgbuf.Length;
                var rect = (Rect2i)roi;
                j["box"][0] = rect.X;
                j["box"][1] = rect.Y;
                j["box"][2] = rect.X + rect.Width;
                j["box"][3] = rect.Y + rect.Height;
                var cen = (rect.TopLeft + rect.BottomRight) / 2;
                cen.Write(j["point"][0]);
                j["label"][0] = 0;
                j["multimask"] = true;
                var str = j.ToString().Replace("\n", "").Replace(" ", "") + "\r\n";
                Logger.Debug(str);

                if(!_client.Connected) throw new Exception("Client not connected!");
                //send request
                var buf = ASCIIEncoding.ASCII.GetBytes(str);
                _client.Send(buf, 0, buf.Length);
                var res = _client.ReadLine(1000);//expect OK 
                if(res != "OK") {
                    throw new Exception($"Failed to send request (RES={res})");
                }
                Logger.Debug(">" + res);
                //send image data
                _client.Send(_imgbuf, 0, _imgbuf.Length);
                res = _client.ReadLine(10000);//expect OK 
                Logger.Debug(">" + res);

                //parse response
                Json jres = Json.FromString(res);
                int mask_id = jres["mask"];
                float score = jres["score"];
                int mask_size = jres["size"];
                byte[] res_buf = new byte[mask_size];
                _client.Read(res_buf, 0, mask_size);
                res = _client.ReadLine(10000);//expect OK 
                if(res != "OK") {
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
                    .ForEach(x => img.DrawPoly(x, Color.Magenta, 2,true));
                iv.SetImage(img);
            } catch(Exception ex) { Logger.Error(ex.Message); Logger.Trace(ex.StackTrace); }
        }
    }
}
