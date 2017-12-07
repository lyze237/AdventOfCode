using System;
using System.IO;

namespace Utils
{
    public abstract class Challenge<TR>
    {
        private readonly FileInfo challengeInput;

        protected Challenge(string challengeName)
        {
            challengeInput = new FileInfo(GetType().Namespace + "/" + challengeName);
            Console.WriteLine($"Input file is {challengeInput.FullName}");
        }
        
        protected string[] GetInputFilePerLine()
        {
            return File.ReadAllLines(challengeInput.FullName);
        }
        
        protected string GetInputFile()
        {
            return File.ReadAllText(challengeInput.FullName);
        }

        public abstract TR Run();
    }
}