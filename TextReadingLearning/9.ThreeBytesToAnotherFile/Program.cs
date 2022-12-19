using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9.ThreeBytesToAnotherFile
{
    public class Program9
    {
        public static void ThreeBytesToAnotherFile(string ReadPath, string WritePath, int ProjNum)
        {
            using (FileStream fr = new FileStream(ReadPath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                int r = 0;
                int i = 1;
                StreamWriter fw = new StreamWriter(WritePath + "\\" + ProjNum + "." + i + ".txt");
                while ((r = fr.ReadByte()) != -1)
                {
                    string s = Convert.ToString(r, 16);
                    if (String.Compare(s, "31") == 0 || String.Compare(s, "32") == 0 || String.Compare(s, "33") == 0)
                    {
                        fw.Flush();
                        fw.Close();
                        i++;
                        fw = new StreamWriter(WritePath + "\\" + ProjNum + "." + i + ".txt");
                    }
                    fw.Write(s);
                    fw.Write('\n');
                }
                fw.Flush();
                fw.Close();
            }
        }
        static void Main(string[] args)
        {
            string ReadPath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Test2.txt";
            string WritePath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg";
            ThreeBytesToAnotherFile(ReadPath, WritePath, 9);

        }
    }
}
