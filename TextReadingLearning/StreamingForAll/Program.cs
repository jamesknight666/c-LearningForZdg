using StreamingForAll;
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
        public string path;
        public Action<string> DataArrived;
        public FileReadStringBlock(string path)
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
        public List<string> PathList=new List<string>();

        public FileWriteStringBlock(string path)
        {
            InputBlock = new ActionBlock<string>(p =>
            {
                //老问题，ActionBlock操作太慢，输入容易被新输入覆盖，没办法，File.AppendAllText比StreamWriter操作快一点
                //using (StreamWriter sw = new StreamWriter(path))
                //{
                //    sw.Write(p);
                //    Console.Write(p);
                //}
                File.AppendAllText(path, p + '\n');
                PathList.Add(path);
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
            });
        }
        public void Enqueue(string input)
        {
            InputBlock.Post(input);
        }
    }

    public class FileReadByteBlock
    {
        public string path;
        public int num;
        public Action<byte[]> DataArrived;
        public FileReadByteBlock(string path, int num)
        {
            this.path = path;
            this.num = num;
        }
        public void Start()
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
                    Thread.Sleep(1 * num);//这里必须要挂起，后面操作不够快的话，前面的输入会不断被新输入覆盖，暂时没有好的解决办法。
                }
            }
        }
    }

    public class FileWriteByteBlock
    {
        public ActionBlock<byte[]> InputBlock;
        public List<string> PathList = new List<string>();
        public FileWriteByteBlock(string path, int jinzhi)
        {
            InputBlock = new ActionBlock<byte[]>(p =>
            {
                //老问题，ActionBlock操作太慢，输入容易被新输入覆盖，没办法，File.AppendAllText比StreamWriter操作快一点
                //using (StreamWriter sw = new StreamWriter(path))
                //{
                //    sw.Write(p);
                //    Console.Write(p);
                //}
                foreach (byte b in p)
                {
                    if(b!=0)
                       File.AppendAllText(path, Convert.ToString(b, jinzhi) + '\n');
                }
                PathList.Add(path);
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
                    if(b!=0)
                        Console.Write(Convert.ToString(b, jinzhi) + " ");
                }
                if (str == "huanhang")
                    Console.Write('\n');
            });
        }
        public void Enqueue(byte[] input)
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

