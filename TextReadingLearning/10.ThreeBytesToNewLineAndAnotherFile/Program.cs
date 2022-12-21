using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _8.ThreeBytesToConsoleNewLine;
using _9.ThreeBytesToAnotherFile;

namespace _10.ThreeBytesToNewLineAndAnotherFile
{
    class Program10
    {
        static void Main()
        {
            string ReadPath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg\\test2.txt";
            string WritePath = "C:\\Users\\DELL\\桌面\\zdg学习\\c#LearningForZdg";
            Program9.ThreeBytesToAnotherFile(ReadPath, WritePath, 10,16);
            Program8.ThreeBytesToConsoleNewLine(ReadPath,16);
        }
    }
}
