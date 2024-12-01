using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2024;

[TestFixture]
public class Day1() : Day<(int[] left, int[] right)>(2024, 1)
{
    protected override object? DoPart1((int[] left, int[] right) input)
    {
        var totalDifference = 0;
        
        for (var i = 0; i < input.left.Length; i++)
        {
            var left = input.left[i];
            var right = input.right[i];
            totalDifference += Math.Abs(left - right);
        }

        return totalDifference;
    }

    protected override object? DoPart2((int[] left, int[] right) input)
    {
        var totalSimilarity = 0;

        foreach (var left in input.left)
        {
            var rightCount = input.right.Count(r => r == left);
            totalSimilarity += left * rightCount;
        }
        
        return totalSimilarity;
    }

    protected override (int[] left, int[] right) ParseInput(string input)
    {
        var arrays = input.Split("\n")
            .Select(line =>
            {
                var (left, right, _) = line.Split("   ");
                return (left: Convert.ToInt32(left), right: Convert.ToInt32(right));
            }).ToArray();

        var left = arrays.Select(a => a.left).OrderBy(a => a).ToArray();
        var right = arrays.Select(a => a.right).OrderBy(a => a).ToArray();
        return (left, right);
    }
}