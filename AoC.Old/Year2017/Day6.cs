using AdventOfCode.Parsers;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day6 : Day<string[]>.WithParser<TabParser>
{
    public override object ExecutePart1()
    {
        var banks = Input.Select(int.Parse).ToArray();
        var configs = new List<int[]>();

        while (!configs.Any(x => x.SequenceEqual(banks)))
        {
            configs.Add(banks.ToArray());
            RedistributeBlocks(banks);
        }

        return configs.Count;
    }
    
    public override object ExecutePart2()
    {
        var banks = Input.Select(int.Parse).ToArray();
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