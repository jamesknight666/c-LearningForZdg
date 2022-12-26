using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Runtime.CompilerServices;
using StreamingForAll;

namespace _1.TextToConsole
{
    class program1
    {
        static void Main()
        {
            //确定读入路径
            FileReadStringBlock FRSB = new FileReadStringBlock("C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\Test2.txt");
            ConsoleWriteStringBlock CWSB = new ConsoleWriteStringBlock();
            //加入方法
            FRSB.DataArrived += (e) =>
            {
                CWSB.Enqueue(e);
            };
            //开始流程
            FRSB.Start();
            Console.ReadKey();
        }
    }
}
