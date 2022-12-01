using Tidy.AdventOfCode;

namespace AdventOfCode.Year2022;

public class Day1 : Day
{
    public override object ExecutePart1() => 
        SortElves().First();

    public override object ExecutePart2() => 
        SortElves().Take(3).Sum();

    private IEnumerable<long> SortElves() =>
        Input
            .Split("\n\n")
            .Select(g => g.Split("\n"))
            .Select(g => g.Sum(Convert.ToInt64))
            .OrderByDescending(g => g);
}