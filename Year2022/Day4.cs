using AdventOfCode.Year2022.Extensins;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2022;

public class Day4 : Day<List<(List<int>, List<int>)>>
{
    public override List<(List<int>, List<int>)> ParseInput(string rawInput)
    {
        return rawInput.Split("\n")
            .Select(line => line.Split(","))
            .Select(entries => entries.Select(entry =>
            {
                var (start, end, _) = entry.Split("-").Select(s => Convert.ToInt32(s)).ToArray();
                return Enumerable.Range(start, end - start + 1).ToList();
                }).ToList())
            .Select(line => (line[0], line[1])).ToList();
    }

    public override object ExecutePart1() => 
        Input.Count(tuple => tuple.Item1.All(t => tuple.Item2.Contains(t)) || tuple.Item2.All(t => tuple.Item1.Contains(t)));

    public override object ExecutePart2() => 
        Input.Count(tuple => tuple.Item1.Any(t => tuple.Item2.Contains(t)) || tuple.Item2.Any(t => tuple.Item1.Contains(t)));
}