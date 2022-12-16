using System;
using System.IO;

using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.ByteToConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(FileStream fs=new FileStream(@"C:\Users\DELL\桌面\zdg学习\c#LearningForZdg\Test1.txt",FileMode.OpenOrCreate,FileAccess.Read))
            {
                byte[] by = new byte[1];
                while(true)
                {
                    int r = fs.Read(by, 0, by.Length);
                    //十六进制另一种写法
                    //Console.WriteLine(BitConverter.ToString(by));
                    foreach (byte b in by)
                    {
                        Console.WriteLine(Convert.ToString(b, 2));
                    }
                    if (r == 0)
                        break;
                }
            }
            Console.ReadKey();


        }
    }
}
