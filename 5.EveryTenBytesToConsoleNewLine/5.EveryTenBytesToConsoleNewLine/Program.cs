using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5.EveryTenBytesToConsoleNewLine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream(@"C:\Users\DELL\桌面\zdg学习\c#LearningForZdg\Test1.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                byte[] by = new byte[10];
                while (true)
                {
                    int r = fs.Read(by, 0, by.Length);
                    foreach (byte b in by)
                    {
                        string s = Convert.ToString(b, 2);
                        Console.Write("{0}  ",s);
                    }
                    Console.Write('\n');
                    if (r == 0)
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
