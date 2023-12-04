using System.Text.RegularExpressions;
using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day3 : Day<Day3.Part[]>
{
    public record Point(int Y, int X);

    public record Symbol(char Type, Point Point);

    public record Part(int Number, Symbol Symbol);

    public Day3() : base(2023, 3)
    {
    }

    protected override object DoPart1(Part[] input) =>
        input.Sum(part => part.Number);

    protected override object DoPart2(Part[] input) =>
        input
            .Where(part => part.Symbol.Type == '*') 
            .GroupBy(part => part.Symbol.Point)
            .Where(group => group.Count() == 2)
            .Sum(group => group.Select(g => g.Number).Mul());

    protected override Part[] ParseInput(string inputString)
    {
        var input = inputString.Split("\n");

        var parts = new List<Part>();
        for (var i = 0; i < input.Length; i++)
        {
            var matches = Regex.Matches(input[i], @"(\d+)");
            foreach (Match match in matches)
            {
                var symbol = IsAdjacentToSymbol(match, i, input);
                if (symbol != null)
                    parts.Add(new Part(match.Value.ToInt(), symbol));
            }
        }

        return parts.ToArray();
    }

    private static Symbol? IsAdjacentToSymbol(Match match, int i, string[] input)
    {
        for (var x = match.Index - 1; x <= match.Index + match.Length; x++)
        {
            for (var y = i - 1; y <= i + 1; y++)
            {
                if (x < 0 || y < 0 || y >= input.Length || x >= input[i].Length)
                    continue;

                var c = input[y][x];
                if (char.IsDigit(c) || c == '.')
                    continue;

                return new Symbol(c, new Point(y, x));
            }
        }

        return null;
    }
}