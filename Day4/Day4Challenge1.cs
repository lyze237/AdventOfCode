using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Day4
{
    public class Day4Challenge1 : Challenge<int>
    {
        public Day4Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            return GetInputFilePerLine().Select(line => line.Split(" ")).Count(wordsArray => new HashSet<string>(wordsArray).Count == wordsArray.Length);
        }
    }
}