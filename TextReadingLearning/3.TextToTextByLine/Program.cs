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
            string[] paths = { "..\\..\\..\\..\\..\\Test1.txt" };
            FileReadStringBlock FRSB = new FileReadStringBlock(paths);
            FileWriteStringBlock FWSB = new FileWriteStringBlock("..\\..\\..\\..\\..\\3.", 0,1);
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
