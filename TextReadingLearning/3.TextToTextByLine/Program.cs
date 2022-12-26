using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Runtime.CompilerServices;
using StreamingForAll;

namespace _3.TextToTextByLine
{
    class program3
    {
        static void Main()
        {
            int i = 0;
            FileReadStringBlock FRSB = new FileReadStringBlock("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Test1.txt");
            FRSB.DataArrived += (e) =>
            {
                FileWriteStringBlock FWSB = new FileWriteStringBlock("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\2."+ ++i +".txt");
                FWSB.Enqueue(e);
            };
            FRSB.Start();
            Console.ReadKey();
        }
    }
}
