using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace _1.TextToConsole
{
    class program
    {
        static void Main()
        {
            string line="";
            using(StreamReader sr=new StreamReader("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Test3.txt"))
            {
                while ((line=sr.ReadLine())!=null)
                {
                    Console.WriteLine(line);
                }
            }
            Console.ReadKey();
        }
    }
}
