using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using StreamingForAll;

namespace _14.DeleteThreeBytesPlusThreeBytesToAnotherFile
{
    public class Program14
    {
        static void Main()
        {
            byte[] by1 = { 0xAA, 0xBB, 0xCC };
            byte[] by2 = { 0x31, 0x32, 0x33 };
            string[] paths = { "..\\..\\..\\..\\..\\Test1.txt" };
            FileReadByteBlock FRBB = new FileReadByteBlock(paths, 116);
            AddBytesForHead ABFH = new AddBytesForHead(by1);
            DeleteForBytesBlock DFBB = new DeleteForBytesBlock(by2);
            FileWriteByteBlock FWBB = new FileWriteByteBlock("..\\..\\..\\..\\..\\14.1.txt", 16, 1, 1);
            FRBB.DataArrived += (e) =>
            {
                ABFH.Enqueue(e);
            };
            ABFH.DataArrived += (e) =>
            {
                DFBB.Enqueue(e);
            };
            DFBB.DataArrived += (e) =>
            {
                FWBB.Enqueue(e);
            };
            FRBB.Start();


            FWBB.InputBlock.Completion.Wait();
            //Console.ReadKey();
        }
    }
}
