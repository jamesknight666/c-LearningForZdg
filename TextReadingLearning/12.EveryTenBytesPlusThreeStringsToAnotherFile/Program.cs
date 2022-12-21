using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _6.EveryTenBytesToAnotherFile;
using Microsoft.VisualBasic;

namespace _12.EveryTenBytesPlusThreeStringsToAnotherFile
{
    public class Program12
    {
        public static List<string> path = new List<string>();
        public static void AddStringsForHeadToAnotherFile(List<string> ReadPaths, string[] strings, int ProjNum)
        {
            int i = 1;
            foreach (string ReadPath in ReadPaths)
            {
                string line;
                StreamReader fr = new StreamReader(ReadPath);
                StreamWriter fw = new StreamWriter("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg" + "\\" + ProjNum + "." + i + ".txt");
                path.Add("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg" + "\\" + ProjNum + "." + i + ".txt");
                foreach (string s in strings)
                    fw.WriteLine(s);
                while ((line = fr.ReadLine()) != null)
                {
                    fw.WriteLine(line);
                }
                fw.Close();
                fr.Close();
                i++;
            }
        }
        static void Main()
        {
            string ReadPath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\test1.txt";
            string WritePath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg";
            string[] strings = { "0xAA", "0xBB", "0xCC" };
            Program6.EveryTenBytesToAnotherFile(ReadPath, WritePath, 6, 2);
            AddStringsForHeadToAnotherFile(Program6.path, strings, 12);
        }
    }
}
