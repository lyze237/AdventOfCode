using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2022;

[TestFixture]
public class Day4 : Day<List<(List<int>, List<int>)>>
{
    public Day4() : base(2022, 4) { }

    protected override object DoPart1(List<(List<int>, List<int>)> input) =>
        input.Count(tuple => tuple.Item1.All(t => tuple.Item2.Contains(t)) || tuple.Item2.All(t => tuple.Item1.Contains(t)));

    protected override object DoPart2(List<(List<int>, List<int>)> input) =>
        input.Count(tuple => tuple.Item1.Any(t => tuple.Item2.Contains(t)) || tuple.Item2.Any(t => tuple.Item1.Contains(t)));

    protected override List<(List<int>, List<int>)> ParseInput(string input) =>
        input.Split("\n")
            .Select(line => line.Split(","))
            .Select(entries => entries.Select(entry =>
            {
                var (start, end, _) = entry.Split("-").Select(s => Convert.ToInt32(s)).ToArray();
                return Enumerable.Range(start, end - start + 1).ToList();
            }).ToList())
            .Select(line => (line[0], line[1])).ToList();
}