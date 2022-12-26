using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamingForAll;

namespace _2.TextToText
{
    class Program2
    {
        static void Main()
        {
            FileReadBlock<char> FRB = new FileReadBlock<char>("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Test2.txt");
            ConsoleWriteBlock<char> CWB = new ConsoleWriteBlock<char>();
            FRB.DataArrived += (e) =>
            {
                CWB.Enqueue(e);
            };
            FRB.Start();
            Console.ReadKey();
        }
    }
}
