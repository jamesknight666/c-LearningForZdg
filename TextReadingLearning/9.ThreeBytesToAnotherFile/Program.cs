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
        public static List<string> path=new List<string>();
        public static void ThreeBytesToAnotherFile(string ReadPath, string WritePath, int ProjNum,int jinzhi)
        {
            using (FileStream fr = new FileStream(ReadPath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                int r = 0;
                int i = 1;
                StreamWriter fw = new StreamWriter(WritePath + "\\" + ProjNum + "." + i + ".txt");
                path.Add(WritePath + "\\" + ProjNum + "." + i + ".txt");
                while ((r = fr.ReadByte()) != -1)
                {
                    string s;
                    if (r != 0x31)
                        fw.Write(Convert.ToString(r, jinzhi));
                    else if ((r = fr.ReadByte()) != 0x32)
                        fw.Write(Convert.ToString(0x31, jinzhi) +'\n'+ Convert.ToString(r, jinzhi));
                    else if ((r = fr.ReadByte()) != 0x33)
                        fw.Write(Convert.ToString(0x31, jinzhi) + '\n' + Convert.ToString(0x32, jinzhi) + '\n' + Convert.ToString(r, jinzhi));
                    else
                    {
                        fw.Flush();
                        fw.Close();
                        i++;
                        fw = new StreamWriter(WritePath + "\\" + ProjNum + "." + i + ".txt");
                        fw.Write(Convert.ToString(0x31, jinzhi) + '\n' + Convert.ToString(0x32, jinzhi) + '\n' + Convert.ToString(0x33, jinzhi));
                        path.Add(WritePath + "\\" + ProjNum + "." + i + ".txt");
                    }
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
            ThreeBytesToAnotherFile(ReadPath, WritePath, 9,16);

        }
    }
}
