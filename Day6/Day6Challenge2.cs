using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Day6
{
    public class Day6Challenge2 : Challenge
    {
        public Day6Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            var banks = GetInputFile().Split("\t").Select(int.Parse).ToArray();
            var configs = new List<int[]>();

            while (!configs.Any(x => x.SequenceEqual(banks)))
            {
                configs.Add((int[])banks.Clone());
                RedistributeBlocks(banks);
            }

            var seenIndex = configs.IndexOf(configs.First(x => x.SequenceEqual(banks)));
            var steps = configs.Count - seenIndex;

            return steps;
        }
        
        private static void RedistributeBlocks(int[] banks)
        {
            var idx = banks.ToList().IndexOf(banks.Max());
            var blocks = banks[idx];

            banks[idx++] = 0;

            while (blocks > 0)
            {
                if (idx >= banks.Length)
                {
                    idx = 0;
                }

                banks[idx++]++;
                blocks--;
            }
        }
    }
}