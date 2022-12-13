using AoC.Framework;
using NUnit.Framework;

namespace AoC._2022;

[TestFixture]
public class Day1 : Day<string>
{
    public Day1() : base(2022, 1) { }

    protected override object DoPart1(string input) => 
        SortElves(input).First();

    protected override object DoPart2(string input) => 
        SortElves(input).Take(3).Sum();

    protected override string ParseInput(string input) => 
        input;

    private static IEnumerable<long> SortElves(string input) =>
        input.Split("\n\n")
            .Select(g => g.Split("\n"))
            .Select(g => g.Sum(Convert.ToInt64))
            .OrderByDescending(g => g);
}