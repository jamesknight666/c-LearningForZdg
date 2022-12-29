using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using StreamingForAll;

namespace _13.DeleteThreeStringsInFiles12
{
    public class Program13
    {
        static void Main()
        {
            byte[] by = { 0xAA, 0xBB, 0xCC };
            int i = 0;
            List<string> paths = new List<string>();
            while (true)
            {
                string path = "..\\..\\..\\..\\..\\12." + ++i + ".txt";
                if (!File.Exists(path))
                    break;
                paths.Add("..\\..\\..\\..\\..\\12." + i + ".txt");
            }
            FileReadStringBlock FRSB = new FileReadStringBlock(paths.ToArray());
            DeleteForBytesBlock DFBB = new DeleteForBytesBlock(by);
            StringsToByteArray STBA = new StringsToByteArray(16);
            FileWriteByteBlock FWBB = new FileWriteByteBlock("..\\..\\..\\..\\..\\13.", 16, 0, 1);
            FRSB.DataArrived += (e) =>
            {
                ;
            };
            FRSB.DataArrived1 += (e) =>
            {
                STBA.Enqueue(e);
            };
            STBA.DataArrived += (e) =>
            {
                DFBB.Enqueue(e);
            };
            DFBB.DataArrived += (e) =>
            {
                FWBB.Enqueue(e);
            };
            FRSB.Start();
            DFBB.InputBlock.Completion.Wait();
            STBA.InputBlock.Completion.Wait();
            FWBB.InputBlock.Completion.Wait();
            //Console.ReadKey();
        }
    }
}
