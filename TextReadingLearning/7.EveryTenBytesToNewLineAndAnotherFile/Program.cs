using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _5.EveryTenBytesToConsoleNewLine;
using _6.EveryTenBytesToAnotherFile;

namespace _7.EveryTenBytesToConsoleNewLineAndAnotherFile
{
    class Program7
    {
        static void Main()
        {
            string ReadPath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\test1.txt";
            string WritePath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg";
            Program6.EveryTenBytesToAnotherFile(ReadPath, WritePath, 7, 2);
            Program5.EveryTenBytesToConsoleNewLine(ReadPath,2);
        }
    }
}
