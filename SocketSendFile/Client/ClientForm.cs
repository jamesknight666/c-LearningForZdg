using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Client
{
    public partial class ClientForm : Form
    {
        public Socket ClientSocket { get; set; }
        public ClientForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            GetIPAddress();
            //1.创建socket
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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

        private void ContBtn_Click(object sender, EventArgs e)
        {
            if (ClientSocket.Connected)
            {
                MessageBox.Show("已连接服务器");
                return;
            }
            //2.连接服务器
            try
            {
                ClientSocket.Connect(IPAddress.Parse(IPText.Text), int.Parse(PortText.Text));
                AppendTextToTextBox(DateTime.Now.ToString() + string.Format("  服务端{0}已连接", ClientSocket.RemoteEndPoint.ToString()));
            }
            catch
            {
                MessageBox.Show("服务器繁忙，请稍后重试");
                return;
            }
            //3.接收消息
            Thread thread = new Thread(ReceiveData);
            thread.IsBackground = true;//放入后台线程
            thread.Start(ClientSocket);
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            if (!ClientSocket.Connected)
            {
                MessageBox.Show("服务器未连接");
                return;
            }
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
            else
            {
                ClientSocket.Send(result);
                AppendTextToTextBox(DateTime.Now.ToString() + string.Format("  客户端：{0}", MsgBox.Text));
                MsgBox.Clear();
            }
        }

        public void ReceiveData(object socket)
        {
            var ProxSocket = socket as Socket;
            while (true)
            {
                byte[] data = new byte[1024 * 1024];
                int len = 0;
                //防止异常退出用try
                try
                {
                    //不断把接受的数据放入缓冲区
                    len = ProxSocket.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch
                {
                    StopConnect();
                    return;
                }
                //把接收到的数据放入TextBox
                if (data[0] == 1)
                {
                    string str = Encoding.Default.GetString(data, 1, len);
                    AppendTextToTextBox(DateTime.Now.ToString() + string.Format("  服务器：{0}", str));
                }
                else if (data[0] == 2)
                {
                    if (MessageBox.Show("是否接受来自" + ProxSocket.RemoteEndPoint.ToString() + "的文件", " ", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        AppendTextToTextBox(DateTime.Now.ToString() + "  取消接收服务器" + ProxSocket.RemoteEndPoint.ToString() + "发送的文件");
                        continue;
                    }
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.InitialDirectory = @"C:\Users\user\Desktop";
                        sfd.DefaultExt = "txt";
                        sfd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";

                        var name = new ArraySegment<byte>(data, 2, data[1]);
                        sfd.FileName = Encoding.Default.GetString(name);

                        if(sfd.ShowDialog(this)==DialogResult.Cancel)
                        {
                            AppendTextToTextBox(DateTime.Now.ToString() + "  取消接收服务器" + ProxSocket.RemoteEndPoint.ToString() + "发送的文件");
                            continue;
                        }
                        byte[] filedata = new byte[len - 2 - data[1]];
                        Buffer.BlockCopy(data, 2 + data[1], filedata, 0, len - 2 - data[1]);
                        File.WriteAllBytes(sfd.FileName, filedata);
                        AppendTextToTextBox(DateTime.Now.ToString() + "  成功接收服务器" + ProxSocket.RemoteEndPoint.ToString() + "：文件" + sfd.FileName);
                    }
                }

            }
        }

        private void StopConnect()
        {
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close(100);
        }

        public void AppendTextToTextBox(string str)
        {
            TextBox.AppendText(str);
            TextBox.AppendText("\r\n");
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ClientSocket.Connected)
            {
                return;
            }
            byte[] data = Encoding.Default.GetBytes("正常退出");
            byte[] result = new byte[data.Length + 1];
            result[0] = 1;
            Buffer.BlockCopy(data, 0, result, 1, data.Length);
            ClientSocket.Send(result);
            //StopConnect();
        }

        private void FileBtn_Click(object sender, EventArgs e)
        {
            byte[] result;
            byte[] name;
            try
            {
                using (OpenFileDialog ofg = new OpenFileDialog())
                {
                    if (ofg.ShowDialog() == DialogResult.Cancel)
                        throw new Exception("取消发送");
                    byte[] data = File.ReadAllBytes(ofg.FileName);
                    name = Encoding.Default.GetBytes(ofg.FileName.Substring(ofg.FileName.LastIndexOf('\\') + 1, ofg.FileName.Length - ofg.FileName.LastIndexOf('\\') - 1));
                    result = new byte[data.Length + name.Length + 2];
                    result[0] = 2;
                    result[1] = (byte)name.Length;
                    Buffer.BlockCopy(name, 0, result, 2, name.Length);
                    Buffer.BlockCopy(data, 0, result, name.Length + 2, data.Length);
                }
                ClientSocket.Send(result);
                AppendTextToTextBox(DateTime.Now.ToString() + "  成功发送给服务器" + ClientSocket.RemoteEndPoint.ToString() + "：文件" + Encoding.Default.GetString(name));
            }
            catch
            {
                return;
            }
        }
    }
}
