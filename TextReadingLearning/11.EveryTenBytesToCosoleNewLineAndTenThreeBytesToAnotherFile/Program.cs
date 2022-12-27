using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using StreamingForAll;

namespace _11.EveryTenBytesToConsoleNewLineAndTenThreeBytesToAnotherFile
{
    public class Program11
    {
        static void Main(string[] args)
        {
            byte[] by = { 0x31, 0x32, 0x33 };
            FileReadByteBlock FRBB = new FileReadByteBlock("..\\..\\..\\..\\..\\Test1.txt", 1);
            CutForBytesBlock CFBB = new CutForBytesBlock(by);
            CutMostNumBytesBlock CMNBB = new CutMostNumBytesBlock(10);
            ConsoleWriteByteBlock CWBB = new ConsoleWriteByteBlock(16, "huanhang");
            FileWriteByteBlock FWBB = new FileWriteByteBlock("..\\..\\..\\..\\..\\11.", 16, 0, 0);
            FRBB.DataArrived += (e) =>
            {
                CFBB.Enqueue(e);
            };
            CFBB.DataArrived += (e) =>
            {
                CMNBB.Enqueue(e);
            };
            CMNBB.DataArrived += (e) =>
            {
                CWBB.Enqueue(e);
                FWBB.Enqueue(e);
            };
            FRBB.Start();

            CFBB.InputBlock.Completion.Wait();
            CMNBB.InputBlock.Completion.Wait();
            CWBB.InputBlock.Completion.Wait();
            FWBB.InputBlock.Completion.Wait();
            //Console.ReadKey();
        }
    }
}
