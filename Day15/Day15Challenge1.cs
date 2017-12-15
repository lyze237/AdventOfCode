using System.Linq;
using System;
using System.Runtime.CompilerServices;
using Utils;

namespace Day15
{
    public class Day15Challenge1 : Challenge<long>
    {
        public Day15Challenge1() : base("input")
        {
        }

        public override long Run()
        {
            var inputFilePerLine = GetInputFilePerLine();
            var genAValue = Convert.ToInt64(inputFilePerLine[0].Split(' ').Last());
            var genBValue = Convert.ToInt64(inputFilePerLine[1].Split(' ').Last());

            int judge = 0;
            for (long i = 0; i < 40_000_000; i++)
            {
                genAValue = CalculateANext(genAValue);
                genBValue = CalculateBNext(genBValue);

                string genA = Convert.ToString(genAValue, 2);
                string genB = Convert.ToString(genBValue, 2);

                if (string.Join("", genA.Reverse().Take(16)) == string.Join("", genB.Reverse().Take(16)))
                {
                    judge++;
                }
            }

            return judge;
        }

        private static long CalculateANext(long val)
        {
            val *= 16807;
            return val % 2147483647;
        }
        
        private static long CalculateBNext(long val)
        {
            val *= 48271;
            return val % 2147483647;
        }
    }
}