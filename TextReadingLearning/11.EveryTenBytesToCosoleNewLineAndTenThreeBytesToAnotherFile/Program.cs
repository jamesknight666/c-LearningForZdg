using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _5.EveryTenBytesToConsoleNewLine;

namespace _11.EveryTenBytesToConsoleNewLineAndTenThreeBytesToAnotherFile
{
    public class Program11
    {
        public static void TenThreeBytesToAnotherFile(string ReadPath, string WritePath, int ProjNum, int jinzhi)
        {
            using (FileStream fr = new FileStream(ReadPath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                int r = 0;
                int i = 1;
                int count = 0;
                StreamWriter fw = new StreamWriter(WritePath + "\\" + ProjNum + "." + i + ".txt");
                while ((r = fr.ReadByte()) != -1)
                {
                    string s = Convert.ToString(r, jinzhi);
                    if (r == 0x31 || r == 0x32 || r == 0x33 || count == 10)
                    {
                        fw.Flush();
                        fw.Close();
                        i++;
                        fw = new StreamWriter(WritePath + "\\" + ProjNum + "." + i + ".txt");
                        count = 0;
                    }
                    fw.Write(s);
                    fw.Write('\n');
                    count++;
                }
                fw.Flush();
                fw.Close();
            }
        }
        static void Main(string[] args)
        {
            string ReadPath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Test2.txt";
            string WritePath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg";
            Program5.EveryTenBytesToConsoleNewLine(ReadPath, 16);
            TenThreeBytesToAnotherFile(ReadPath, WritePath, 11, 16);

        }
    }
}
