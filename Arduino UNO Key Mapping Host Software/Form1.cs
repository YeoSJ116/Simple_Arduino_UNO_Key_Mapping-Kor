using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using System.IO.Ports;//아두이노와의 통신을 하기위해 필요한 선언입니다.


/// SPARROW 1.0.1 Release Version - Kor
/// <summary>
/// 릴리즈 노트
/// 재작자: 여 상준(Sang-Jun Yeo)
/// 국  가: 대한민국(Republic of Korea)
/// 
/// 2018_02_27
/// :: SPARROW 1.0.0r - Kor
/// 사소한 버그 해결
/// 실행 안됨, Fixform설정 등등...
/// 
/// 2017_11_18
/// :: SPARROW 1.0.0r - Kor
/// 이 버전은 간단하게 실행 해 볼 수 있습니다.
/// </summary>

namespace Arduino_UNO_Key_Mapping_Host_Software
{
    public partial class Form1 : Form
    {
        //아두이노 통신속도 변수값 (Hard Coding)
        int SerialSpeed = 9600;

        //Cross Thread 호출오류를 해결하기 위한 코드입니다.
        private delegate void TextData(string sData);
        private void RXvalue(string sData) { RXmsg.Text = sData; }

        //아두이노와 통신하기 위한 Socket을 생성하는 코드입니다.
        private SerialPort ArduinoSP;
        void Socket_Setup()
        {
            try
            {
                SerialPort ArduinoSP = new SerialPort();
            }
            catch
            {
                MessageBox.Show("아두이노 연결 상태 확인 및 연결후 실행해 주세요", "아두이노 연결오류");
            }
        }
        //가상키를 운영체제에 전달하기위한 메소드 선언입니다.
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, ref int dwExtralnfo);
        private int Info=0;

        //가상키 코드값 입니다. (ASCII)
        private const int KEYDOWN = 0x100;
        private const int KEYUP = 0x101;
        private const int Enter_key = 0x0D;
        //--키보드 누름-------------//
        private const int A_key = 0x41;
        private const int B_key = 0x42;
        private const int C_key = 0x43;
        private const int D_key = 0x44;
        private const int E_key = 0x45;
        //--키보드 땜---------------//
        private const int a_key = 0x61;
        private const int b_key = 0x62;
        private const int c_key = 0x63;
        private const int d_key = 0x64;
        private const int e_key = 0x65;

        public Form1()
        {
            InitializeComponent();
            Socket_Setup();
            //단순히 처음 실행시 포트 목록을 보여주는 코드입니다.
            portlist.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            portlist.Items.AddRange(ports);
            portlist.SelectedIndex = 0;
        }
        //포트 목록을 다시 불러옵니다.
        private void refresh_Click(object sender, EventArgs e)
        {
            portlist.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            portlist.Items.AddRange(ports);
        }
        //연결을 시도합니다.
        //만약 프로그램이 정상적으로 동작하지 않는 경우 아두이노 포트번호를 확인해 주세요
        private void connectArdoino_Click(object sender, EventArgs e)
        {
            if(ArduinoSP.IsOpen == false)
            {
                try
                {
                    ArduinoSP.PortName = portlist.Text;
                    ArduinoSP.BaudRate = SerialSpeed;
                    ArduinoSP.Open();
                    connectArdoino.Text = "연결됨\n클릭하여 연결해지";
                }
                catch
                {
                    MessageBox.Show("올바른 아두이노 포트인지 확인해 주세요", "연결 오류!");
                }
            }else
            {
                ArduinoSP.Close();
                connectArdoino.Text = "연결";
            }
        }

        //프로그램 종료시 자동적으로 연결을 해지하기 위한 코드입니다.
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ArduinoSP.IsOpen == true) ArduinoSP.Close();
        }

        //아두이노 시리얼 통신을 받으면 실행되는 코드입니다.
        //DataReceive >>>stirng>>> Append
        void ArduinoSP_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Append(ArduinoSP.ReadExisting());
        }

        //DataReceive >>>stirng>>> Append
        void Append(String Read)
        {
            //만약 조금더 빠른 반응속도를 원하시다면 Invoke코드를 제거(또는 주석처리)하셔도 됩니다.
            Invoke(new TextData(RXvalue), Read); //Winform에서 "수신:"에게 내용을 전달합니다. (Invoke)

            //Keys down:: Keys press
            if (Read.Contains("A"))
            {
                keybd_event(A_key, 0, KEYDOWN, ref Info);
            }
            if (Read.Contains("B"))
            {
                keybd_event(B_key, 0, KEYDOWN, ref Info);
            }
            if (Read.Contains("C"))
            {
                keybd_event(C_key, 0, KEYDOWN, ref Info);
            }
            if (Read.Contains("D"))
            {
                keybd_event(D_key, 0, KEYDOWN, ref Info);
            }
            if (Read.Contains("E"))
            {
                keybd_event(E_key, 0, KEYDOWN, ref Info);
            }

            //Keys up
            if (Read.Contains("1"))
            {
                keybd_event(a_key, 0, KEYUP, ref Info);
            }
            if (Read.Contains("2"))
            {
                keybd_event(b_key, 0, KEYUP, ref Info);
            }
            if (Read.Contains("3"))
            {
                keybd_event(c_key, 0, KEYUP, ref Info);
            }
            if (Read.Contains("4"))
            {
                keybd_event(d_key, 0, KEYUP, ref Info);
            }
            if (Read.Contains("5"))
            {
                keybd_event(e_key, 0, KEYUP, ref Info);
            }
        }

        //숨김
        private void hideForm_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            this.Hide();
            notifyIcon1.ShowBalloonTip(10);
        }
        //보여주기
        private void notifyIcon1_MouseClick(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
        }
        //보여주기
        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
        }

        //사진 클릭시 프로그램 정보를 표시하기위한 코드입니다.
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Programe_Info InfoForm = new Programe_Info();
            InfoForm.Show();
        }
    }
}
