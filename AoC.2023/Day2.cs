using System.Text.RegularExpressions;
using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day2 : Day<Day2.Game[]>
{
    public record Dice(int Number, string Color);

    public record Game(int Id, Dice[] Picks);

    public Day2() : base(2023, 2)
    {
    }

    protected override object DoPart1(Game[] input) => 
        input.Where(game => game.Picks.All(IsGood)).Sum(game => game.Id);

    private static bool IsGood(Dice dice) =>
        dice switch
        {
            (Number: <= 12, Color: "red") => true,
            (Number: <= 13, Color: "green") => true,
            (Number: <= 14, Color: "blue") => true,
            _ => false
        };

    protected override object DoPart2(Game[] input) =>
        input.Sum(game => game.Picks.GroupBy(d => d.Color)
            .Select(groups => groups.Max(d => d.Number))
            .Mul());

    protected override Game[] ParseInput(string input) =>
        (from line in input.Split("\n")
            let id = Regex.Match(line, @"Game (?<id>\d+):").Groups["id"].Value.ToInt()
            let rounds = Regex.Matches(line, @"(?<number>\d+) (?<color>\w+)")
                .Select(die => new Dice(die.Groups["number"].Value.ToInt(), die.Groups["color"].Value))
                .ToArray()
            select new Game(id, rounds)).ToArray();
}