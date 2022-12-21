using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.EveryTenBytesToAnotherFile
{
    public class Program6
    {
       public static List<string> path=new List<string>();
       public static void EveryTenBytesToAnotherFile(string ReadPath, string WritePath, int ProjNum,int jinzhi)
        {
            using (FileStream fr = new FileStream(ReadPath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                int i = 1;
                while (true)
                {
                    byte[] by = new byte[10];
                    int r = fr.Read(by, 0, by.Length);
                    if (r == 0)
                        break;
                    StreamWriter fw = new StreamWriter(WritePath + "\\" + ProjNum + "." + i + ".txt");
                    path.Add(WritePath + "\\" + ProjNum + "." + i + ".txt");
                    foreach (byte b in by)
                    {
                        string s = Convert.ToString(b, jinzhi);
                        if (s != "0")
                            fw.WriteLine("{0}", s);
                    }
                    fw.Flush();
                    fw.Close();
                    i++;
                }
            }
        }
        static void Main()
        {
                string ReadPath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\test1.txt";
                string WritePath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg";
                EveryTenBytesToAnotherFile(ReadPath, WritePath, 6,2);
        }
    }
}