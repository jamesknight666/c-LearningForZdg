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

namespace Client
{
    public partial class ClientForm : Form
    {
        public Socket ClientSocket { get; set; }
        public ClientForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void ContBtn_Click(object sender, EventArgs e)
        {
            //1.创建socket
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ClientSocket = socket;
            //2.连接服务器
            try
            {
                socket.Connect(IPAddress.Parse(IPText.Text), int.Parse(PortText.Text));
                AppendTextToTextBox(string.Format("服务端{0}已连接", ClientSocket.RemoteEndPoint.ToString()));
            }
            catch (Exception ex)
            {
                //TextBox.Show("请稍后重试");
                return;
            }
            //3.接收消息
            Thread thread = new Thread(new ParameterizedThreadStart(ReceiveData));
            thread.IsBackground = true;
            thread.Start(ClientSocket);
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            if (ClientSocket.Connected)
            {
                if (string.IsNullOrEmpty(MsgBox.Text))
                {
                    MessageBox.Show("发送信息不能为空");
                    return;
                }
                byte[] data = Encoding.Default.GetBytes(MsgBox.Text);
                ClientSocket.Send(data);
                MsgBox.Text = null;
            }
        }

        public void ReceiveData(object socket)
        {
            var ProxSocket = socket as Socket;
            byte[] data = new byte[1024 * 1024];
            while (true)
            {
                int len = 0;
                //防止异常退出用try
                try
                {
                    //不断把接受的数据放入缓冲区
                    len = ProxSocket.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch (Exception ex)
                {
                    ////客户端异常退出
                    //AppendTextToTextBox(string.Format("服务端{0}异常退出", ProxSocket.RemoteEndPoint.ToString()));
                    ////停止连接，并结束线程
                    //StopConnect();
                    //return;
                }
                if (len <= 0)
                {
                    //客户端正常退出
                    AppendTextToTextBox(string.Format("服务端{0}正常退出", ProxSocket.RemoteEndPoint.ToString()));
                    //停止连接，并结束线程
                    StopConnect();
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

        private void StopConnect()
        {
            try
            {
                if (ClientSocket.Connected)
                {
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close(100);
                }
            }
            catch (Exception ex)
            {


            }

        }

        public void AppendTextToTextBox(string str)
        {
            //判断是否跨线程
            if (TextBox.InvokeRequired)
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

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppendTextToTextBox(string.Format("服务端{0}正常退出", ClientSocket.RemoteEndPoint.ToString()));
            //判断是否已连接，如果连接就关闭连接
            StopConnect();
        }

        private void FileBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofg = new OpenFileDialog())
            {
                if (ofg.ShowDialog() != DialogResult.OK)
                    return;
                byte[] data = File.ReadAllBytes(ofg.FileName);
                byte[] result = new byte[data.Length + 1];
                result[0] = 2;
                Buffer.BlockCopy(data, 0, result, 1, data.Length);
                ClientSocket.Send(result);
            }
        }
    }
}
