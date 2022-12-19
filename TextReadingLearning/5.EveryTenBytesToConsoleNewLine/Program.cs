using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5.EveryTenBytesToConsoleNewLine
{
    public class Program5
    {
        public static void EveryTenBytesToConsoleNewLine(string ReadPath)
        {
            using (FileStream fs = new FileStream(ReadPath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                while (true)
                {
                    byte[] by = new byte[10];
                    int r = fs.Read(by, 0, by.Length);
                    if (r == 0)
                        break;
                    foreach (byte b in by)
                    {
                        string s = Convert.ToString(b, 2);
                        if (s != "0")
                            Console.Write("{0}  ", s);
                    }
                    Console.Write('\n');
                }
            }
        }
        static void Main(string[] args)
        {
            string ReadPath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Test1.txt";
            EveryTenBytesToConsoleNewLine(ReadPath);
        }
    }
}
