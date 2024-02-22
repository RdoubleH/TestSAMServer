using LotusAPI;
using LotusAPI.Controls;
using LotusAPI.Data;
using LotusAPI.Math;
using LotusAPI.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Img = LotusAPI.MV.Image;

namespace TestSAMServer {
    public partial class Form1 : Form {
        SimpleTcpClient _client = new SimpleTcpClient(); //클라이언트 생성
        //Tcp : 서버와 클라이언트간 데이터를 전달하기위해 만들어진 연결기반 프로토콜(연결이 성립되면 양방향 통신 가능)
        //IP : 호스트의 주소지정, 패킷의 분할 및 조립 프로토콜
        //Socket : 클라이언트와 서버, 두 컴퓨터가 특정한 Port를 통해 실시간으로, 양방향 통신을 가능하게 만든 통신
        //HTTP 통신 : 두 컴퓨터가 연결을 지속하는 방식이 아닌 클라이언트의 요청이 수신 되었을 때 서버에서 연결을 허가해 대응하는 방식, 서버에서 클라이언트로 요청 불가
        // - socket은 이미 연결되어 연결을 생성하고 해제하는 과정이 없어 리소스 낭비를 없애고 서버에서도 요청 가능
        //서버와 클라이언트 : 서버는 요청에 대한 응답, 클라이언트는 서버에 요청 및 응답을 받아 처리
        public Form1() {
            InitializeComponent(); //메인 화면 구성
            LotusAPI.Registry.SetApplicationName("ATMC\\Ex\\TestSAMServer");
            Logger.Add(new LogViewLogger(logView1));
            Library.Initialize(); //options = 3; //사용할Library를 plugin 하는 작업
            //None = 0,
            //UseDongle = 1,
            //UseHwPlugins = 2,
            //Default = 3,
            //All = 65535
        }

        void Connect() {
            _client.Disconnect(); //클라이언트 연결 해제
            _client = new SimpleTcpClient(); //클라이언트 생성
            _client.Connect(tb_IP.Text, 8727, 10000, 10000); //클라이언트 연결 (IP, 포트번호, receive_timeout, send_timeout)
            if(_client.Connected) //연결됨
                Logger.Info("CONNECTED");
            else
                throw new Exception("Failed to connect!"); //연결 안됨
        }
        void Disconnect() {
            _client?.Disconnect(); //?의 의미는 nullable : 값이 null일 수도 있다.
            //client 연결 해제
        }

        LotusAPI.MV.Image _img;

        String GetJsonStr(Json j) => j.ToString().Replace("\n", "").Replace(" ", "") + "\r\n";
        //json에 띄어쓰기, 줄바꿈 삭제 후 Json데이터 쓰고 줄바꿈
        void SendRequest(Json request) {
            if(!_client.Connected) throw new Exception("Client not connected!"); //클라이언트 연결 안됨
            var str = GetJsonStr(request);
            var buf = ASCIIEncoding.ASCII.GetBytes(str); //가공한 Json 데이터를 ASCII 변환
            _client.Send(buf, 0, buf.Length); //Socket.send로 데이터 전달, Socket은 컴퓨터끼리 통신하는 프로그래밍 방식
            var res = _client.ReadLine(1000);//expect OK 정상적으로 전달되면 OK
            if(res != "OK") {
                throw new Exception($"Failed to send request (RES={res})");
            }
            Logger.Debug(">" + res);
        }

        void SetImage(Img img) {
            //convert image to PNG buffer
            byte[] imgbuf = img.EncodePNG(); //이미지를 png 변환
            Json request = new Json(); //Json 프레임 생성
            request["name"] = "set_img";
            request["size"] = imgbuf.Length;
            //name, size 할당
            SendRequest(request);
            _client.Send(imgbuf, 0, imgbuf.Length); //imgbuf와 index, length를 소켓으로 전달
            Logger.Debug(imgbuf.ToString());
            Logger.Debug(imgbuf.ToString());
            Logger.Debug(imgbuf.Length.ToString());
            Logger.Debug(imgbuf.Length.ToString());

            var res = _client.ReadLine(100000);//expect OK , timeout : 100초
            Logger.Debug(res);//Rhh
            if (res != "OK") {
                throw new Exception($"Failed to send request (RES={res})");
            }
            Logger.Debug(">" + res);
        }

        private void bt_LOAD_Click(object sender, EventArgs e) {
            //Server에 {"name":"set_img","size":2332784}를 넘겨줌
            try
            {
                _img = DialogUtils.OpenImage("Open image").ToBGR();
                //내PC 라이브러리에서 가져온 이미지를 BGR형태로 변환
                iv.SetImage(_img); //FastImageView를 사용해 iv에 이미지를 할당 //Server에 연결했을 때 auto_fit인지 확인해볼것
                Connect(); //8727포트로 서버에 접속
                SetImage(_img); //SetImage() 호출
                iv.AutoScale(); //사진을 iv에 맞게 사이즈 맞춤
                Predict(); //sam 수행
            } catch(Exception ex) { Logger.Error(ex.Message); Logger.Trace(ex.StackTrace); }
            Disconnect(); //Rhh
        }

        List<Point2i> _pnts = new List<Point2i>();
        List<Point2i> _neg_pnts = new List<Point2i>();
        RectangleF _roi;
        private void iv_ROISelectedEvent(RectangleF roi) //드래그, Ctrl + 좌클릭으로 선택한 사각형 값
        { //InitializeComponent()함수 호출 시 동작
            _roi = roi;
            if (ckb_InROI.Checked) Predict(_roi, _pnts, _neg_pnts); //ckb_InROI : Find in ROI 버튼
        }

        private void Predict() => Predict(_roi, _pnts, _neg_pnts);
        //이미지와 관심영역(ROI)를 이용해 미리 학습된 모델을 사용하여 예측을 수행하고 결과를 표시하는 기능
        private void Predict(RectangleF roi, List<Point2i> pnts, List<Point2i> neg_pnts) {
            //_roi : 예측을 위한 관심영역, _pnts : 관심영역 내의 점 목록, _neg_pnts : 관심영역 밖의 점 목록
            Logger.Debug(roi.ToString());//Rhh
            Logger.Debug(pnts.ToString());//Rhh
            Logger.Debug(neg_pnts.ToString());//Rhh
            try {
                if(_img == null) throw new Exception("No image");// _img가 null이면
                //create request message
                Json request = new Json(); //Json 형태 생성
                request["name"] = "predict"; //key:name, value:predict
                var rect = (Rect2i)roi; //Rect2i타입의 roi 생성
                Logger.Debug(rect.ToString());//Rhh
                Logger.Debug(roi.Width.ToString());//Rhh
                Logger.Debug(roi.Height.ToString());//Rhh
                if (roi.Width * roi.Height > 100) { //roi(RectangleF)의 가로 * 세로가 100보다 크면 key:box, value : roi사각형 point 4개를 Json에 저장 
                    request["box"][0] = rect.X;
                    request["box"][1] = rect.Y;
                    request["box"][2] = rect.X + rect.Width;
                    request["box"][3] = rect.Y + rect.Height;
                }
               
                else if(pnts != null && pnts.Count > 0)
                {//pnts가 null이 아니고 count가 0보다 크면
                    Logger.Debug(pnts.Count.ToString());//Rhh
                    for (int i = 0; i < pnts.Count; i++) { //count만큼 point를 보냄
                        pnts[i].Write(request["point"][i]);
                        request["label"][i] = 1; //key: label[i], value는 항상 1
                    }
                    if(neg_pnts != null) { //neg_pnts값이 null이 아니면
                        Logger.Debug(neg_pnts.Count.ToString());//Rhh
                        for (int i = 0; i < neg_pnts.Count; i++)
                        { //neg_pnts count만큼 point를 보냄
                            neg_pnts[i].Write(request["point"][i + pnts.Count]);
                            request["label"][i + pnts.Count] = 0; //pnts의 index와 구분하기 위해 pnts의 index를 더함
                        }
                    }
                }
                else { //상위 조건에 해당되지 않으면 iv.SetImage()호출
                    iv.SetImage(_img);
                    return;
                }

                request["multimask"] = false; //multimask , value : false
                var str = request.ToString().Replace("\n", "").Replace(" ", "") + "\r\n"; //request 정리
                Logger.Debug(str);

                Connect(); //Rhh

                //send request
                var buf = ASCIIEncoding.ASCII.GetBytes(str); //정리한 request값을 ASCII로 변환
                _client.Send(buf, 0, buf.Length); //Server로 전송
                var res = _client.ReadLine(1000);//expect OK , runtime 1초
                if(res != "OK") {
                    throw new Exception($"Failed to send request (RES={res})");
                }
                Logger.Debug(">" + res);

                res = _client.ReadLine(100000);//expect OK , runtime 100초, json데이터를 분리 시켜 StringBuilder에 누적시켜 서버에 보낸 결과값
                //parse response
                Logger.Debug(res);//서버 연결해서 확인해보기
                Json jres = Json.FromString(res); //FromString에 결과값을 넣어 변환시킨 값
                int mask_id = jres["mask"]; 
                float score = jres["score"]; 
                int mask_size = jres["size"];
                Logger.Debug(mask_id.ToString()); //Rhh
                Logger.Debug(score.ToString()); //Rhh
                Logger.Debug(mask_size.ToString()); //Rhh
                byte[] res_buf = new byte[mask_size]; //Json데이터의 size값
                Logger.Debug(res_buf.ToString());//Rhh
                _client.Read(res_buf, 0, mask_size); // Read()로mask_img의 데이터를 Server에 보내서 값을 받아옴
                res = _client.ReadLine(1000);//expect OK 
                if(res != "OK") {
                    throw new Exception($"Failed to receive result data (RES={res})");
                }
                Logger.Debug(">" + res);
                //read mask data, res_buf 이미지값을 회색조로 encode : 사람이 해석할 수 있는 데이터를 컴퓨터가 해석할 수 있는 binary format으로 변환, decode는 반대
                var mask_img = LotusAPI.MV.Image.Decode(res_buf, LotusAPI.MV.ImageDecodeOption.Grayscale).ToGray();
                
                //create overlay
                Logger.Debug(_img.ToString());//Rhh
                var img = _img.Clone() as LotusAPI.MV.Image; //_img 복사
                Logger.Debug(img.ToString());//Rhh
                var bgr = img.Split(); //이미지 분리
                Logger.Debug(bgr.ToString()); //Rhh
                //blend result
                img = LotusAPI.MV.Image.Merge(new LotusAPI.MV.Image[] { bgr[0], bgr[1] * 0.5 + mask_img * 0.5, bgr[2] }); //원본이미지와 선택한 이미지 병합
                //draw outline
                mask_img.FindContours(20) //20 : min size, 윤곽선이나 가장자리를 찾는 이미지
                    .ToList() //윤곽의 모음을 반환
                    .ForEach(x => img.DrawPoly(x, Color.Magenta, 2, true)); //다각형 그리기, 윤곽 목록으로 다각형 생성
                // (list[i], color, thickness, isClosed ) 첫 점과 끝점을 연결해 닫는 두께 2픽셀 짜리 자홍색 다각형 생성 
                iv.SetImage(img);
            } catch(Exception ex) { Logger.Error(ex.Message); Logger.Trace(ex.StackTrace); }
            Disconnect(); //Rhh
        }



        private void iv_MouseClick(FastImageView sender, FastImageView.MouseEventArgs e) {
            if(ckb_InROI.Checked) return; //Find in ROI가 켜져있으면 안됨
            _roi = new RectangleF(); //roi로 사용할 사각형클래스 생성 
            if(ModifierKeys == Keys.Control) { //Ctrl키
                _pnts.Add(e.Location); //imageview에서 마우스클릭이 일어난 point(X,Y) 값
                Predict();
            }
            if(ModifierKeys == Keys.Shift) { //Shift
                _neg_pnts.Add(e.Location); //imageview에서 마우스클릭이 일어난 point(X,Y) 값
                Predict();
            }
            iv.Invalidate(); //변경된 값으로 화면 재구성
        }

        private void iv_PostRenderDrawEvent(FastImageView c) { //이미지를 그린 후에 원을 그림
            try {
                //_pnts의 수만큼 원을 그림
                for(int i = 0; i < _pnts.Count; i++) {
                    c.DrawCircle(_pnts[i], 6, Color.Lime, 3);//circle center, radius, color, width(선의 굵기)
                }
                for(int i = 0; i < _neg_pnts.Count; i++) {
                    c.DrawCircle(_neg_pnts[i], 6, Color.Red, 3);
                }
                c.DrawRectangle(_roi, Color.Magenta, 2); //RectangleF 타입의 rect, color, width(선의 굵기)
            } catch { }
        }

        private void iv_KeyUp(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Escape) { //Esc 키를 눌렀을 때
                _pnts.Clear(); //초기화
                _neg_pnts.Clear(); //초기화
            }
            Predict(); 
            iv.Invalidate(); //변경된 값으로 iv화면 재구성 iv redraw
        }
    }
}
