using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;

namespace Server
{
    public partial class ServerForm : Form
    {
        List<Socket> ProxSocketList = new List<Socket>();
        public ServerForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            AppendTextToTextBox(string.Format("服务器已开机"));
            //1.创建欢迎socket（IPV4，流，TCP协议）
            Socket welsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //2.绑定端口IP(Parse分别转化为地址和int)
            welsocket.Bind(new IPEndPoint(IPAddress.Parse(IPText.Text),int.Parse(PortText.Text)));

            //3.开始监听,等待连接队列个数为10，同时连接的个数不设限制
            welsocket.Listen(10);

            //4.开始接受客户端连接（Accept()是阻塞式，所以要放入线程池）
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.AcceptClientConnect), welsocket);
        }

        public void AcceptClientConnect(object socket)                                                               
        {
            var ServerSocket=socket as Socket;
            while(true)
            {
                //接受连接之后就需要用代理socket通信，一个客户端对应一个代理。
                var ProxSocket = ServerSocket.Accept();
                AppendTextToTextBox(string.Format("客户端{0}已连接上", ProxSocket.RemoteEndPoint.ToString()));
                ProxSocketList.Add(ProxSocket);

                //接受来自客户端的消息。Receive()函数也是阻塞式的，因此也要放入线程池。
                ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveData), ProxSocket);
            }

        }

        public void ReceiveData(object socket)
        {
            var ProxSocket = socket as Socket;
            byte[] data=new byte[1024*1024];
            while(true)
            {
                int len = 0;
                //防止异常退出用try
                try
                {
                    //不断把接受的数据放入缓冲区
                    len = ProxSocket.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch(Exception ex)
                {
                    //客户端异常退出
                    AppendTextToTextBox(string.Format("客户端{0}异常退出", ProxSocket.RemoteEndPoint.ToString()));
                    //将socket移出List，并结束线程
                    ProxSocketList.Remove(ProxSocket);
                    StopConnect(ProxSocket);
                    return;
                }
                if(len<=0)
                {
                    //客户端正常退出
                    AppendTextToTextBox(string.Format("客户端{0}正常退出", ProxSocket.RemoteEndPoint.ToString()));
                    //将socket移出List，并结束线程
                    ProxSocketList.Remove(ProxSocket);
                    StopConnect(ProxSocket);
                    return;
                }
                //把接收到的数据放入TextBox
                if (data[0] == 1)
                {
                    string str = Encoding.Default.GetString(data, 1, len);
                    AppendTextToTextBox(string.Format("客户端{0}：{1}", ProxSocket.RemoteEndPoint.ToString(), str));
                }
                else if (data[0] == 2)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.DefaultExt = "txt";
                        sfd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";

                        if (sfd.ShowDialog(this) != DialogResult.OK)
                            return;
                        byte[] filedata = new byte[len - 1];
                        Buffer.BlockCopy(data, 1, filedata, 0, len - 1);
                        File.WriteAllBytes(sfd.FileName, filedata);
                    }
                }
            }
        }

        private void StopConnect(Socket ProxSocket)
        {
            try
            {
                if (ProxSocket.Connected)
                {
                    ProxSocket.Shutdown(SocketShutdown.Both);
                    ProxSocket.Close(100);
                }
            }
            catch (Exception ex)
            {


            }

        }

        public void AppendTextToTextBox(string str)
        {
            //判断是否跨线程
            if(TextBox.InvokeRequired)
            {
                TextBox.BeginInvoke(new Action<string>(s =>
                {
                    TextBox.Text = string.Format("{0}\r\n{1}", TextBox.Text, str);
                }
                ), str);
                ////同步方法
                //TextBox.Invoke(new Action<string>(s =>
                //{
                //    this.TextBox.Text = string.Format("{0}\r\n{1}",str,TextBox.Text);
                //}
                //), str);
            }
            else
            {
                TextBox.Text = string.Format("{0}\r\n{1}", TextBox.Text, str);
            }
        }
        private void SendBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MsgBox.Text))
            {
                MessageBox.Show("发送信息不能为空");
                return;
            }

            foreach (var ProxSocket in ProxSocketList)
            {
                if(ProxSocket.Connected)
                {
                    //原始字符串转化为字节数组
                    byte[] data = Encoding.Default.GetBytes(MsgBox.Text);

                    //对原始数组加上协议头部：1代表字符串，2代表文件
                    byte[] result = new byte[data.Length + 1];
                    result[0] = 1;

                    //把data的数据拷入result后面
                    Buffer.BlockCopy(data, 0, result, 1, data.Length);
                    ProxSocket.Send(data);
                }
            }
            MsgBox.Text = null;
        }

        private void FileBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofg = new OpenFileDialog())
            {
                if (ofg.ShowDialog() != DialogResult.OK)
                    return;
                byte[] data=File.ReadAllBytes(ofg.FileName);
                byte[] result= new byte[data.Length+1];
                result[0] = 2;
                Buffer.BlockCopy(data, 0, result, 1, data.Length);

                foreach (var ProxSocket in ProxSocketList)
                {
                    if (ProxSocket.Connected)
                    {
                        ProxSocket.Send(result);
                    }
                }
            }
        }
    }
}
