using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day9 : Day<long[][]>
{
    public Day9() : base(2023, 9, true)
    {
    }

    protected override object DoPart1(long[][] input) =>
        input.Select(Solve).Sum();
    
    protected override object DoPart2(long[][] input) =>
        input.Select(i => i.Reverse().ToArray()).Select(Solve).Sum();

    private static long Solve(long[] line) => 
        !line.Any() ? 0 : Solve(FindNextValues(line)) + line.Last();

    private static long[] FindNextValues(long[] line) => 
        line.Zip(line.Skip(1)).Select(l => l.Second - l.First).ToArray();

    protected override long[][] ParseInput(string input) => 
        input
            .Split("\n")
            .Select(line => line.Split(" ").Select(i => i.ToLong()).ToArray())
            .ToArray();
}