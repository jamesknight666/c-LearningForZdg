using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Runtime.CompilerServices;
using StreamingForAll;

namespace _2.TextToText
{
    class program2
    {
        static void Main()
        {
            FileReadStringBlock FRSB = new FileReadStringBlock("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Test2.txt");
            FileWriteStringBlock FWSB = new FileWriteStringBlock("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\TextToText.txt");
            FRSB.DataArrived += (e) =>
            {
                FWSB.Enqueue(e);
            };
            FRSB.Start();
            Console.ReadKey();
        }
    }
}
