using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _9.ThreeBytesToAnotherFile;
using _5.EveryTenBytesToConsoleNewLine;

namespace _11.EveryTenBytesToConsoleNewLineAndTenThreeBytesToAnotherFile
{
    public class Program11
    {
        public static List<string> path = new List<string>();
        public static void CutTenBytesToAnotherFile(List<string> ReadPaths, string WritePath, int ProjNum, int jinzhi)
        {
            int i = 0;
            foreach (string ReadPath in ReadPaths)
            {
                string line;
                int count = 0;
                StreamReader fr = new StreamReader(ReadPath);
                StreamWriter fw = new StreamWriter(WritePath + "\\" + ProjNum + "." + ++i + ".txt");
                path.Add(WritePath + "\\" + ProjNum + "." + i + ".txt");
                //有点问题，不知道为什么到第三个文件就写不进去了。
                /*
                if (i == 3)
                    fw.WriteLine("!!!!!!!!!!!!!!!!!!");
                */
                while ((line = fr.ReadLine()) != null)
                {
                    fw.WriteLine(line);
                    count++;
                    if (count == 10)
                    {
                        fw = new StreamWriter(WritePath + "\\" + ProjNum + "." + ++i + ".txt");
                        path.Add(WritePath + "\\" + ProjNum + "." + i + ".txt");
                        count = 0;
                    }
                }
                fw.Close();
                fr.Close();
            }
        }
        static void Main(string[] args)
        {
            string ReadPath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Test6.txt";
            string WritePath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Output";
            Program5.EveryTenBytesToConsoleNewLine(ReadPath, 16);
            Program9.ThreeBytesToAnotherFile(ReadPath, WritePath, 9, 16);
            CutTenBytesToAnotherFile(Program9.path, WritePath, 11, 16);
        }
    }
}
