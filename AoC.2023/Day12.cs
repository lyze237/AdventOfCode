using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day12 : Day<(string map, int[] format)[]>
{
    public Day12() : base(2023, 12)
    {
    }

    protected override object DoPart1((string map, int[] format)[] input) => 
        input.Sum(tuple => Solve(tuple.map, tuple.format));

    protected override object DoPart2((string map, int[] format)[] input) =>
        input
            .Select(tuple => (map: string.Join("?", new[] { tuple.map }.Repeat(5)), format: tuple.format.Repeat(5).ToArray()))
            .Sum(tuple => Solve(tuple.map, tuple.format));

    private static long Solve(string map, IReadOnlyList<int> format) =>
        Solve(new Dictionary<(int, int, int), long>(), map, format, 0, 0, 0);
    
    private static long Solve(IDictionary<(int, int, int), long> blockMap, string map, IReadOnlyList<int> format, int mapIndex, int formatIndex, int currentIndex)
    {
        if (blockMap.TryGetValue((mapIndex, formatIndex, currentIndex), out var num))
            return num;

        if (mapIndex == map.Length)
            return (formatIndex == format.Count && currentIndex == 0) || (formatIndex == format.Count - 1 && format[formatIndex] == currentIndex) ? 1 : 0;

        var total = 0L;
        switch (map[mapIndex])
        {
            case '.' or '?' when currentIndex == 0:
                total += Solve(blockMap, map, format, mapIndex + 1, formatIndex, 0);
                break;
            case '.' or '?' when currentIndex > 0 && formatIndex < format.Count && format[formatIndex] == currentIndex:
                total += Solve(blockMap, map, format, mapIndex + 1, formatIndex + 1, 0);
                break;
        }

        if (map[mapIndex] is '#' or '?')
            total += Solve(blockMap, map, format, mapIndex + 1, formatIndex, currentIndex + 1);

        blockMap.Add((mapIndex, formatIndex, currentIndex), total);
        return total;
    }

    protected override (string map, int[] format)[] ParseInput(string input) =>
        input.Split("\n").Select(line =>
        {
            var (first, second, _) = line.Split(" ");
            return (first!, second!.Split(",").Select(s => s.ToInt()).ToArray());
        }).ToArray();
}