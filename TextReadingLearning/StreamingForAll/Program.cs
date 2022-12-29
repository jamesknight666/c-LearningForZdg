using StreamingForAll;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;
namespace StreamingForAll
{
    /*
        public class FileReadBlock<T>
        {
            public string path;
            public Action<T> DataArrived;
            public FileReadBlock(string path)
            {
                this.path = path;
            }
            public void Start()
            {
                int r;
                string s;
                if (typeof(T) == typeof(char))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while ((r = sr.Read()) != -1)
                        {
                            DataArrived((T)Convert.ChangeType(r, typeof(T)));
                        }
                    }
                }
                else if (typeof(T) == typeof(byte))
                {
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        while ((r = fs.ReadByte()) != -1)
                        {
                            DataArrived((T)Convert.ChangeType(r, typeof(T)));
                        }

                    }
                }
                else if (typeof(T) == typeof(string))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while ((s = sr.ReadLine()) != null)
                        {
                            DataArrived((T)Convert.ChangeType(s, typeof(T)));
                        }
                    }
                }
            }
        }

        public class FileWriteBlock<T>
        {
            public ActionBlock<T> InputBlock;
            public FileWriteBlock(string path)
            {
                InputBlock = new ActionBlock<T>(p =>
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.Write(p);
                        Console.Write(p);
                    }
                });
            }
            public void Enqueue(T input)
            {
                InputBlock.Post(input);
            }
        }

        public class ConsoleWriteBlock<T>
        {
            public ActionBlock<T> InputBlock;
            public ConsoleWriteBlock()
            {
                InputBlock = new ActionBlock<T>(p =>
                {
                    Console.Write(p);
                });
            }
            public void Enqueue(T input)
            {
                InputBlock.Post(input);
            }
        }
    */

    public class FileReadStringBlock
    {
        public string[] paths;
        public Action<string> DataArrived;
        public Action<string[]> DataArrived1;
        public FileReadStringBlock(string[] paths)
        {
            this.paths = paths;
        }
        public void Start()
        {
            foreach(string path in paths)
            {
                List<string> NextInputList = new List<string>();
                using (StreamReader sr = new StreamReader(path))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        NextInputList.Add(s);
                        DataArrived(s);
                    }
                    DataArrived1(NextInputList.ToArray());
                }
            }
        }
    }

    public class FileReadStringBlock1
    {
        public string path;
        public Action<string> DataArrived;
        public FileReadStringBlock1(string path)
        {
            this.path = path;
        }
        public void Start()
        {
            string s;
                using (StreamReader sr = new StreamReader(path))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        DataArrived(s);
                    }
                }
        }
    }

    public class FileWriteStringBlock
    {
        public ActionBlock<string> InputBlock;
        public int i = 0;

        public FileWriteStringBlock(string path, int count, int flag)
        {
            InputBlock = new ActionBlock<string>(p =>
            {
                if (count == 1)
                {
                    File.AppendAllText(path, p + '\n');
                    if (flag == 1)
                        Console.WriteLine("已写入文件" + path);
                }
                else if (count == 0)
                {
                    StreamWriter sw = new StreamWriter(path + ++i + ".txt");
                    sw.WriteLine(p);
                    sw.Flush();
                    if (flag == 1)
                        Console.WriteLine("已写入文件" + path + i + ".txt");
                }
                if (InputBlock.InputCount == 0)
                {
                    if (flag == 1)
                        Console.WriteLine("已全部写入文件");
                }
            });
        }
        public void Enqueue(string input)
        {
            InputBlock.Post(input);
        }
    }

    public class ConsoleWriteStringBlock
    {
        public ActionBlock<string> InputBlock;
        public ConsoleWriteStringBlock()
        {
            InputBlock = new ActionBlock<string>(p =>
            {
                Console.WriteLine(p);
                if (InputBlock.InputCount == 0)
                    Console.WriteLine("已全部写完");
            });
        }
        public void Enqueue(string input)
        {
            InputBlock.Post(input);
        }
    }

    public class FileReadByteBlock
    {
        public string[] paths;
        public int num;
        public Action<byte[]> DataArrived;
        public FileReadByteBlock(string[] paths, int num)
        {
            this.paths = paths;
            this.num = num;
        }
        public void Start()
        {
            foreach(string path in paths)
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    while (true)
                    {
                        byte[] by = new byte[num];
                        int r = fs.Read(by, 0, by.Length);
                        if (r == 0)
                            break;
                        DataArrived(by);
                    }
                }
            }
        }
    }

    public class FileWriteByteBlock
    {
        public ActionBlock<byte[]> InputBlock;
        public int i = 0;
        public FileWriteByteBlock(string path, int jinzhi, int count, int flag)
        {
            InputBlock = new ActionBlock<byte[]>(p =>
            {
                if (count == 1)
                {
                    foreach (byte b in p)
                    {
                        if (b != 0)
                            File.AppendAllText(path, Convert.ToString(b, jinzhi) + '\n'); ;
                    }
                    if (flag == 1)
                        Console.WriteLine("已写入文件" + path);
                }
                else if (count == 0)
                {
                    StreamWriter sw = new StreamWriter(path + ++i + ".txt");
                    foreach (byte b in p)
                    {
                        if (b != 0)
                            sw.WriteLine(Convert.ToString(b, jinzhi));
                    }
                    sw.Flush();
                    if (flag == 1)
                        Console.WriteLine("已写入文件" + path + i + ".txt");
                }
                if (InputBlock.InputCount == 0)
                {
                    if (flag == 1)
                        Console.WriteLine("已全部写入文件");
                }
            });
        }
        public void Enqueue(byte[] input)
        {
            InputBlock.Post(input);
        }
    }

    public class ConsoleWriteByteBlock
    {
        public ActionBlock<byte[]> InputBlock;
        public ConsoleWriteByteBlock(int jinzhi, string str = "buhuan")
        {
            InputBlock = new ActionBlock<byte[]>(p =>
            {
                foreach (byte b in p)
                {
                    if (b != 0)
                        Console.Write(Convert.ToString(b, jinzhi) + " ");
                }
                if (str == "huanhang")
                    Console.Write('\n');
                if (InputBlock.InputCount == 0)
                    Console.WriteLine("已全部写完");
            });
        }
        public void Enqueue(byte[] input)
        {
            InputBlock.Post(input);
        }
    }

    public class CutForBytesBlock
    {
        public ActionBlock<byte[]> InputBlock;
        public List<byte> NextInputList = new List<byte>();
        public List<byte> huanchong = new List<byte>();
        public Action<byte[]> DataArrived;
        public void xieru()
        {
            foreach (byte b in huanchong)
                NextInputList.Add(b);
        }
        public CutForBytesBlock(byte[] bytes)
        {
            int flag = 0;
            int geshu = bytes.Length;
            InputBlock = new ActionBlock<byte[]>(p =>
            {
                byte b = p[0];
                int count = 0;
                for (int i = 0; i < geshu; i++)
                {
                    if (b == bytes[i])
                    {
                        if (flag == i)
                        {
                            huanchong.Add(b);
                            flag++;
                        }
                        else
                        {
                            if (b == bytes[0])
                            {
                                flag = 1;
                                xieru();
                                huanchong.Clear();
                                huanchong.Add(b);
                            }
                            flag = 0;
                            huanchong.Add(b);
                            xieru();
                            huanchong.Clear();
                        }
                    }
                    else
                        count++;
                }
                if (count == geshu)
                {
                    huanchong.Add(b);
                    xieru();
                    huanchong.Clear();
                    flag = 0;
                }
                if (flag == geshu || InputBlock.InputCount == 0)
                {
                    DataArrived(NextInputList.ToArray());
                    NextInputList.Clear();
                    flag = 0;
                    xieru();
                    huanchong.Clear();
                }
            });
        }
        public void Enqueue(byte[] input)
        {
            InputBlock.Post(input);
        }
    }

    public class CutMostNumBytesBlock
    {
        public ActionBlock<byte[]> InputBlock;
        public List<byte> NextInputList = new List<byte>();
        public Action<byte[]> DataArrived;
        public CutMostNumBytesBlock(int Num)
        {
            InputBlock = new ActionBlock<byte[]>(p =>
            {
                int count = 0;
                for (int i = 0; i < p.Length; i++)
                {
                    count++;
                    NextInputList.Add(p[i]);
                    if ((i + 1) % Num == 0)
                    {
                        DataArrived(NextInputList.ToArray());
                        NextInputList.Clear();
                    }
                }
                if (count % Num != 0)
                {
                    DataArrived(NextInputList.ToArray());
                    NextInputList.Clear();
                }
            });
        }
        public void Enqueue(byte[] input)
        {
            InputBlock.Post(input);
        }
    }

    public class DeleteForBytesBlock
    {
        public ActionBlock<byte[]> InputBlock;
        public List<int> UnderDelete = new List<int>();
        public Action<byte[]> DataArrived;
        public DeleteForBytesBlock(byte[] bytes)
        {
            InputBlock = new ActionBlock<byte[]>(p =>
            {
                List<int> UnderDelete = new List<int>();
                List<byte> NextInputList = new List<byte>();
                for (int i = 0; i < p.Length; i++)
                {
                    int flag = 1;
                    if (p[i] == bytes[0])
                    {
                        int j = i;
                        for (int k = 1; k < bytes.Length; k++)
                        {
                            if (p[++j] == bytes[k])
                                flag=2;
                            else
                            {
                                flag = 0;
                                break;
                            }
                        }
                    }
                    if(flag ==2)
                    {
                        for (int k = 0; k < bytes.Length; k++)
                            UnderDelete.Add(i + k);
                        i = i + bytes.Length;
                    }
                }
                foreach (byte b in p)
                {
                    NextInputList.Add(b);
                }
                for(int i=UnderDelete.Count-1;i>=0;i--)
                {
                    NextInputList.RemoveAt(UnderDelete[i]);
                    if (i == 0)
                        break;
                }
                DataArrived(NextInputList.ToArray());
                //NextInputList.Clear();
            });
        }
        public void Enqueue(byte[] input)
        {
            InputBlock.Post(input);
        }
    }
    public class AddBytesForHead
    {
        public ActionBlock<byte[]> InputBlock;
        public List<byte> NextInputList = new List<byte>();
        public Action<byte[]> DataArrived;
        public AddBytesForHead(byte[] by)
        {
            InputBlock = new ActionBlock<byte[]>(p =>
            {
                foreach (byte b in by)
                    NextInputList.Add(b);
                foreach(byte b in p)
                    NextInputList.Add(b);
                DataArrived(NextInputList.ToArray());
                NextInputList.Clear();
            });
        }
        public void Enqueue(byte[] input)
        {
            InputBlock.Post(input);
        }
    }

    public class StringsToByteArray
    {
        public ActionBlock<string[]> InputBlock;
        public Action<byte[]> DataArrived;
        public StringsToByteArray(int jinzhi)
        {
            InputBlock = new ActionBlock<string[]>(p =>
            {
                List<byte> NextInputList = new List<byte>();
                foreach (string s in p)
                {
                    NextInputList.Add(Convert.ToByte(s, jinzhi));
                }
                DataArrived(NextInputList.ToArray());
            });
        }

        public void Enqueue(string[] input)
        {
            InputBlock.Post(input);
        }

    }
    class useless
    {
        static void Main()
        {
            //必须要有个空main在这里，不然引用这个命名空间的有的project跑不了，不知道为什么
        }
    }
}

