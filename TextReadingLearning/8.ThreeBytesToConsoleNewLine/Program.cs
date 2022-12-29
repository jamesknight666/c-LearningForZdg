using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using StreamingForAll;

namespace _8.ThreeBytesToConsoleNewLine
{
    public class Program8
    {
        static void Main(string[] args)
        {
            byte[] by = { 0x31, 0x32, 0x33 };
            string[] paths = { "..\\..\\..\\..\\..\\Test1.txt" };
            FileReadByteBlock FRBB = new FileReadByteBlock(paths, 1);
            CutForBytesBlock CFBB= new CutForBytesBlock(by);
            ConsoleWriteByteBlock CWBB = new ConsoleWriteByteBlock(16,"huanhang");
            FRBB.DataArrived += (e) =>
            {
                CFBB.Enqueue(e);
            };
            CFBB.DataArrived += (e) =>
            {
                CWBB.Enqueue(e);
            };
            FRBB.Start();

            CWBB.InputBlock.Completion.Wait();
            //Console.ReadKey();
        }
    }
}
