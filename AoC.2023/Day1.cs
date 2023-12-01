using System.Text.RegularExpressions;
using AoC.Framework;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day1 : Day
{
    public Day1() : base(2023, 1, true)
    {
    }

    protected override object DoPart1(string[] input) =>
        Run(input, @"\d");

    protected override object DoPart2(string[] input) =>
        Run(input, @"one|two|three|four|five|six|seven|eight|nine|\d");

    private static int Run(IEnumerable<string> input, string regex) =>
        input.Select(line =>
            ConvertMatch(Regex.Match(line, regex).Value) * 10 +
            ConvertMatch(Regex.Match(line, regex, RegexOptions.RightToLeft).Value)).Sum();

    private static int ConvertMatch(string str) => str switch
    {
        "one" => 1,
        "two" => 2,
        "three" => 3,
        "four" => 4,
        "five" => 5,
        "six" => 6,
        "seven" => 7,
        "eight" => 8,
        "nine" => 9,
        _ => Convert.ToInt32(str)
    };
}