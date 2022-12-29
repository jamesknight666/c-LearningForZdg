using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using StreamingForAll;

namespace _9.ThreeBytesToAnotherFile
{
    public class Program9
    {
        static void Main(string[] args)
        {
            byte[] by = { 0x31, 0x32, 0x33 };
            string[] paths = { "..\\..\\..\\..\\..\\Test1.txt" };
            FileReadByteBlock FRBB = new FileReadByteBlock(paths, 1);
            CutForBytesBlock CFBB = new CutForBytesBlock(by);
            FileWriteByteBlock FWBB = new FileWriteByteBlock("..\\..\\..\\..\\..\\9.", 16,0,1);
            FRBB.DataArrived += (e) =>
            {
                CFBB.Enqueue(e);
            };
            CFBB.DataArrived += (e) =>
            {
                FWBB.Enqueue(e);
            };
            FRBB.Start();

            FWBB.InputBlock.Completion.Wait();
            //Console.ReadKey();
        }
    }
}
