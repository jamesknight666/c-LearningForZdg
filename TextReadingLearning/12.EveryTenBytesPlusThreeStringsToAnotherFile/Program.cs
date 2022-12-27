using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _6.EveryTenBytesToAnotherFile;
using Microsoft.VisualBasic;
using StreamingForAll;

namespace _12.EveryTenBytesPlusThreeStringsToAnotherFile
{
    public class Program12
    {
        static void Main()
        {
            byte[] by = { 0xAA, 0xBB, 0xCC };
            FileReadByteBlock FRBB = new FileReadByteBlock("..\\..\\..\\..\\..\\Test1.txt", 10);
            AddBytesForHead ABFH = new AddBytesForHead(by);
            FileWriteByteBlock FWBB = new FileWriteByteBlock("..\\..\\..\\..\\..\\12.", 16, 0, 1);
            FRBB.DataArrived += (e) =>
            {
                ABFH.Enqueue(e);
            };
            ABFH.DataArrived += (e) =>
            {
                FWBB.Enqueue(e);
            };
            FRBB.Start();

            ABFH.InputBlock.Completion.Wait();
            FWBB.InputBlock.Completion.Wait();
            FWBB.InputBlock.Completion.Wait();
            //Console.ReadKey();
        }
    }
}
