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
        Dictionary<string, Socket> ProxSocketDic = new Dictionary<string, Socket>();
        Socket welsocket { get; set; }
        public ServerForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            ClientListBox.Items.Insert(0, "--请选择客户端--");
            ClientListBox.SelectedIndex = 0;
            ClientListBox.Items.Add("全部客户端");
            GetIPAddress();
        }

        private void GetIPAddress()
        {
            //获取本机名称
            string hostName = Dns.GetHostName();

            //获取本机ip（包括ipv4和ipv6）
            IPAddress[] ipList = Dns.GetHostAddresses(hostName);

            foreach (IPAddress ip in ipList)
            {
                //判断是不是IPV4，因为后面的welsocket用的就是IPV4
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    //如果是的话，就加入到IPText
                    IPText.Text = ip.ToString().Trim();//Trim()可以将字符串前后的空白字符删去
                }
            }
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (welsocket != null)
            {
                MessageBox.Show("已开机");
                return;
            }
            AppendTextToTextBox(DateTime.Now.ToString() + "  服务器已开机");
            //1.创建欢迎socket（IPV4，流，TCP协议）
            welsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //2.绑定端口IP(Parse分别转化为地址和int)
            welsocket.Bind(new IPEndPoint(IPAddress.Parse(IPText.Text), int.Parse(PortText.Text.Trim())));

            //3.开始监听,等待连接队列个数为10，同时连接的个数不设限制
            welsocket.Listen(10);

            //4.开始接受客户端连接（Accept()是阻塞式，所以要放入线程池）
            ThreadPool.QueueUserWorkItem(AcceptClientConnect, welsocket);
        }

        public void AcceptClientConnect(object socket)
        {
            var ServerSocket = socket as Socket;
            while (true)
            {
                //接受连接之后就需要用代理socket通信，一个客户端对应一个代理。
                var ProxSocket = ServerSocket.Accept();
                AppendTextToTextBox(DateTime.Now.ToString() + "  客户端" + ProxSocket.RemoteEndPoint.ToString() + "已连接");
                ProxSocketDic.Add(ProxSocket.RemoteEndPoint.ToString(), ProxSocket);
                ClientListBox.Items.Add(ProxSocket.RemoteEndPoint.ToString());

                //接受来自客户端的消息。Receive()函数也是阻塞式的，因此也要放入线程池。
                ThreadPool.QueueUserWorkItem(ReceiveData, ProxSocket);
            }
        }

        public void ReceiveData(object socket)
        {
            var ProxSocket = socket as Socket;
            while (true)
            {
                int len = 0;
                byte[] data = new byte[1024 * 1024];
                //不断把接受的数据放入缓冲区
                try
                {
                    len = ProxSocket.Receive(data);
                }
                catch
                {
                    StopConnect(ProxSocket);
                    return;
                }
                //把接收到的数据放入TextBox
                if (data[0] == 1)
                {
                    string str = Encoding.Default.GetString(data, 1, len);
                    AppendTextToTextBox(DateTime.Now.ToString() + "  客户端" + ProxSocket.RemoteEndPoint.ToString() + "：" + str);
                }
                else if (data[0] == 2)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.InitialDirectory = @"C:\Users\user\Desktop";
                        sfd.DefaultExt = "txt";
                        sfd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";

                        sfd.ShowDialog();
                        byte[] filedata = new byte[len - 1];
                        Buffer.BlockCopy(data, 1, filedata, 0, len - 1);
                        File.WriteAllBytes(sfd.FileName, filedata);
                    }
                }
            }
        }

        private void StopConnect(Socket ProxSocket)
        {
            ProxSocketDic.Remove(ProxSocket.RemoteEndPoint.ToString());
            ClientListBox.Items.Remove(ProxSocket.RemoteEndPoint.ToString());
            ProxSocket.Shutdown(SocketShutdown.Both);
            ProxSocket.Close(100);
        }

        public void AppendTextToTextBox(string str)
        {
            TextBox.AppendText(str);
            TextBox.AppendText("\r\n");
        }
        private void SendBtn_Click(object sender, EventArgs e)
        {
            //原始字符串转化为字节数组
            byte[] data = Encoding.Default.GetBytes(MsgBox.Text);

            //对原始数组加上协议头部：1代表字符串，2代表文件
            byte[] result = new byte[data.Length + 1];
            result[0] = 1;

            //把data的数据拷入result后面
            Buffer.BlockCopy(data, 0, result, 1, data.Length);

            if (string.IsNullOrEmpty(MsgBox.Text))
            {
                MessageBox.Show("发送信息不能为空");
                return;
            }
            else if (ClientListBox.SelectedIndex < 1)
            {
                MessageBox.Show("请选择要发送的客户端");
                return;
            }
            else if (ClientListBox.SelectedIndex == 1)
            {
                if (ClientListBox.Items.Count == 2)
                {
                    MessageBox.Show("没有需要发送的客户端");
                    return;
                }
                else
                {
                    foreach (var ProxSocket in ProxSocketDic)
                    {
                        ProxSocket.Value.Send(result);
                    }
                    AppendTextToTextBox(DateTime.Now.ToString() + "  广播所有客户端：" + MsgBox.Text);
                    MsgBox.Clear();
                }
            }
            else
            {
                ProxSocketDic[ClientListBox.Text].Send(result);
                AppendTextToTextBox(DateTime.Now.ToString() + "  发送给客户端" + ProxSocketDic[ClientListBox.Text].RemoteEndPoint.ToString() + ":" + MsgBox.Text);
                MsgBox.Clear();
            }
        }

        private void FileBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofg = new OpenFileDialog())
            {
                byte[] data = File.ReadAllBytes(ofg.FileName);
                byte[] result = new byte[data.Length + 1];
                result[0] = 2;
                Buffer.BlockCopy(data, 0, result, 1, data.Length);

                if (ClientListBox.SelectedIndex < 1)
                {
                    MessageBox.Show("请选择要发送的客户端");
                    return;
                }
                else if (ClientListBox.SelectedIndex == 1)
                {
                    if (ClientListBox.Items.Count == 2)
                    {
                        MessageBox.Show("没有需要发送的客户端");
                        return;
                    }
                    else
                    {
                        foreach (var ProxSocket in ProxSocketDic)
                        {
                            ProxSocket.Value.Send(result);
                        }
                        MsgBox.Clear();
                    }
                }
                else
                {
                    ProxSocketDic[ClientListBox.Text].Send(result);
                    MsgBox.Clear();
                }
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            byte[] data = Encoding.Default.GetBytes("正常退出");
            byte[] result = new byte[data.Length + 1];
            result[0] = 1;
            Buffer.BlockCopy(data, 0, result, 1, data.Length);
            foreach (var ProxSocket in ProxSocketDic)
            {
                if (ProxSocket.Value.Connected)
                {
                    ProxSocket.Value.Send(result);
                    //StopConnect(ProxSocket.Value);
                }
            }
        }
    }
}
