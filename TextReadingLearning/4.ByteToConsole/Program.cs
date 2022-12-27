using System;
using System.IO;

using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamingForAll;

namespace _4.ByteToConsole
{
    internal class Program4
    {
        static void Main(string[] args)
        {
            //确定读入路径和每次读入byte个数
            FileReadByteBlock FRBB = new FileReadByteBlock("..\\..\\..\\..\\..\\Test1.txt", 10);
            //确定要几进制输出
            ConsoleWriteByteBlock CWBB = new ConsoleWriteByteBlock(16);
            FRBB.DataArrived += (e) =>
            {
                CWBB.Enqueue(e);
            };
            FRBB.Start();
            CWBB.InputBlock.Complete();
            CWBB.InputBlock.Completion.Wait();
            //Console.ReadKey();
        }
    }
}
