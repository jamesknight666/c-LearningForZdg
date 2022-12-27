using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using StreamingForAll;

namespace _10.ThreeBytesToNewLineAndAnotherFile
{
    class Program10
    {
        static void Main()
        {
            byte[] by = { 0x31, 0x32, 0x33 };
            FileReadByteBlock FRBB = new FileReadByteBlock("..\\..\\..\\..\\..\\Test1.txt", 1);
            CutForBytesBlock CFBB = new CutForBytesBlock(by);
            ConsoleWriteByteBlock CWBB = new ConsoleWriteByteBlock(16, "huanhang");
            FileWriteByteBlock FWBB = new FileWriteByteBlock("..\\..\\..\\..\\..\\10.", 16, 0, 0);
            FRBB.DataArrived += (e) =>
            {
                CFBB.Enqueue(e);
            };
            CFBB.DataArrived += (e) =>
            {
                FWBB.Enqueue(e);
                CWBB.Enqueue(e);
            };
            FRBB.Start();

            FWBB.InputBlock.Completion.Wait();
            CWBB.InputBlock.Completion.Wait();
            //Console.ReadKey();
        }
    }
}
