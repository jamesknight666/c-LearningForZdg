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
            FileReadStringBlock FRSB = new FileReadStringBlock("..\\..\\..\\..\\..\\Test1.txt");
            //路径+输入1：所有流写入一个文件。输入0；不同流写入不同文件+输入1；显示写入进度。输入0；不显示
            FileWriteStringBlock FWSB = new FileWriteStringBlock("..\\..\\..\\..\\..\\TextToText.txt",1,1);
            FRSB.DataArrived += (e) =>
            {
                FWSB.Enqueue(e);
            };
            FRSB.Start();
            FWSB.InputBlock.Complete();
            FWSB.InputBlock.Completion.Wait();
            //Console.ReadKey();
        }
    }
}
