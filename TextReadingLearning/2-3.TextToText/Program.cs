using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _23.TextToText
{
    class Program23
    {
        static void Main()
        {
            StreamReader sr = new StreamReader("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Test1.txt");
            StreamWriter sw = new StreamWriter("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\TextToText.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                sw.WriteLine(line);
                Console.WriteLine(line);
            }
            sw.Flush();
            sw.Close();
            Console.ReadKey();
        }
    }
}
