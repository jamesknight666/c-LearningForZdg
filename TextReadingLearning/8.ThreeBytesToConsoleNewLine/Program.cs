using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _8.ThreeBytesToConsoleNewLine
{
    public class Program8
    {
        public static void ThreeBytesToConsoleNewLine(string ReadPath, int jinzhi)
        {
            using (FileStream fs = new FileStream(ReadPath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                int r = 0;
                while ((r = fs.ReadByte()) != -1)
                {
                    string s;
                    if (r != 0x31)
                        Console.Write(Convert.ToString(r, jinzhi) + " ");
                    else if ((r = fs.ReadByte()) != 0x32)
                        Console.Write(Convert.ToString(0x31, jinzhi) + " " + Convert.ToString(r, jinzhi) + " ");
                    else if ((r = fs.ReadByte()) != 0x33)
                        Console.Write(Convert.ToString(0x31, jinzhi) + " " + Convert.ToString(0x32, jinzhi) + " " + Convert.ToString(r, jinzhi) + " ");
                    else 
                    {
                        Console.Write('\n');
                        Console.Write(Convert.ToString(0x31, jinzhi) + " " + Convert.ToString(0x32, jinzhi) + " " + Convert.ToString(0x33, jinzhi) + " ");
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            string ReadPath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Test2.txt";
            ThreeBytesToConsoleNewLine(ReadPath, 16);
        }
    }
}
