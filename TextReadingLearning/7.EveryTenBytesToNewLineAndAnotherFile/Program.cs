using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using StreamingForAll;

namespace _7.EveryTenBytesToConsoleNewLineAndAnotherFile
{
    class Program7
    {
        static void Main()
        {
            int i = 0;
            FileReadByteBlock FRBB = new FileReadByteBlock("..\\..\\..\\..\\..\\Test1.txt", 10);
            ConsoleWriteByteBlock CWBB = new ConsoleWriteByteBlock(16, "huanhang");
            //因为命令行也要显示输出，所以就不显示输出文件的进度了。
            FileWriteByteBlock FWBB = new FileWriteByteBlock("..\\..\\..\\..\\..\\7.", 16,0,0);
            FRBB.DataArrived += (e) =>
            {
                CWBB.Enqueue(e);
                FWBB.Enqueue(e);
            };
            FRBB.Start();
            CWBB.InputBlock.Complete();
            CWBB.InputBlock.Completion.Wait(); 
            FWBB.InputBlock.Complete();
            FWBB.InputBlock.Completion.Wait();
            //Console.ReadKey();
        }
    }
}
