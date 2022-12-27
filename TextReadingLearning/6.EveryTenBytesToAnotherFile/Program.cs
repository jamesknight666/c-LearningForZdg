using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using StreamingForAll;

namespace _6.EveryTenBytesToAnotherFile
{
    public class Program6
    {
        static void Main()
        {
            int i = 0;
            FileReadByteBlock FRBB = new FileReadByteBlock("..\\..\\..\\..\\..\\Test1.txt", 10);
            //（path，进制，写入一个文件还是不同文件）
            FileWriteByteBlock FWBB = new FileWriteByteBlock("..\\..\\..\\..\\..\\6.", 16,0,1);
            FRBB.DataArrived += (e) =>
            {
                FWBB.Enqueue(e);
            };
            FRBB.Start();
            FWBB.InputBlock.Complete();
            FWBB.InputBlock.Completion.Wait();
            //Console.ReadKey();
        }
    }
}