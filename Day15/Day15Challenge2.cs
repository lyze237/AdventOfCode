using System.Linq;
using System;
using System.Collections.Generic;
using Utils;

namespace Day15
{
    public class Day15Challenge2 : Challenge<long>
    {
        public Day15Challenge2() : base("input")
        {
        }

        public override long Run()
        {
            var inputFilePerLine = GetInputFilePerLine();
            var genAValue = Values(Convert.ToInt64(inputFilePerLine[0].Split(' ').Last()), 16807, 4);
            var genBValue = Values(Convert.ToInt64(inputFilePerLine[1].Split(' ').Last()), 48271, 8);

            int judge = 0;
            for (long i = 0; i < 5_000_000; i++)
            {
                genAValue.MoveNext();
                genBValue.MoveNext();

                if (genAValue.Current == genBValue.Current)
                {
                    judge++;
                }
            }

            return judge;
        }

        private static IEnumerator<string> Values(long current, long factor, long modulo)
        {
            while (true)
            {
                current = current * factor % 2147483647;
                if (current % modulo == 0)
                    yield return string.Join("", Convert.ToString(current, 2).Reverse().Take(16));
            }
        }
    }
}