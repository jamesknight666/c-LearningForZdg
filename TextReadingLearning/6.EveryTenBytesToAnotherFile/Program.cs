using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using StreamingForAll;

namespace _6.EveryTenBytesToAnotherFile
{
    public class Program6
    {
        static void Main()
        {
            int i = 0;
            FileReadByteBlock FRBB = new FileReadByteBlock("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Test1.txt",10);
            FRBB.DataArrived += (e) =>
            {
                FileWriteByteBlock FWBB = new FileWriteByteBlock("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\6." + ++i + ".txt",16);
                FWBB.Enqueue(e);
            };
            FRBB.Start();
            Console.ReadKey();
        }
    }
}