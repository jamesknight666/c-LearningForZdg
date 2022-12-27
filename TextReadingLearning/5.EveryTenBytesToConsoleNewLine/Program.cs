﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using StreamingForAll;

namespace _5.EveryTenBytesToConsoleNewLine
{
    public class Program5
    {
        static void Main(string[] args)
        {
            FileReadByteBlock FRBB = new FileReadByteBlock("..\\..\\..\\..\\..\\Test1.txt", 10);
            ConsoleWriteByteBlock CWBB = new ConsoleWriteByteBlock(16,"huanhang");
            FRBB.DataArrived += (e) =>
            {
                CWBB.Enqueue(e);
            };
            FRBB.Start();
            CWBB.InputBlock.Complete();
            CWBB.InputBlock.Completion.Wait();
            //Console.ReadKey();
        }
    }
}
