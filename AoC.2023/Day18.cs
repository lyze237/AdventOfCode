using System.Text.RegularExpressions;
using AoC.Framework;
using AoC.Framework.Data;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day18 : Day<Day18.Instruction[]>
{
    public record Instruction(Direction Direction, int Amount, string Color)
    {
        public long FixedAmount => Convert.ToInt64($"0x{Color[..5]}", 16);

        public Direction FixedDirection => Color.Last() switch
        {
            '0' => Direction.Right,
            '1' => Direction.Down,
            '2' => Direction.Left,
            '3' => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(Direction), Color)
        };
    }

    public Day18() : base(2023, 18)
    {
    }

    protected override object DoPart1(Instruction[] input) => 
        Solve(input, instruction => instruction.Direction, instruction => instruction.Amount);

    protected override object DoPart2(Instruction[] input) => 
        Solve(input, instruction => instruction.FixedDirection, instruction => instruction.FixedAmount);

    private static long Solve(Instruction[] input, Func<Instruction, Direction> directionSelector, Func<Instruction, long> amountSelector)
    {
        var points = new List<Point> { new(0, 0) };
        points.AddRange(input.Select(instruction => points.Last().Move(directionSelector(instruction), amountSelector(instruction))));

        var area = 0L;
        for (var i = 0; i < points.Count - 1; i++)
        {
            area += points[i].X * points[i + 1].Y;
            area -= points[i].Y * points[i + 1].X;
        }

        area += input.Sum(amountSelector);

        return area / 2 + 1;
    }

    protected override Instruction[] ParseInput(string input) =>
        input.Split("\n")
            .Select(line => Regex.Match(line, @"(?<direction>\w+) (?<amount>\d+) \(#(?<color>[\d\w]+)\)"))
            .Select(match => new Instruction(match.Groups["direction"].Value switch
            {
                "U" => Direction.Up,
                "D" => Direction.Down,
                "L" => Direction.Left,
                "R" => Direction.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(match), match.Groups["direction"].Value)
            }, match.Groups["amount"].Value.ToInt(), match.Groups["color"].Value)).ToArray();
}