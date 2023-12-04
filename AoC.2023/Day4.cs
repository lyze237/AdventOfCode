using System.Text.RegularExpressions;
using AoC.Framework;
using AoC.Framework.Extensions;

namespace AoC._2023;

public class Day4 : Day<Day4.Card[]>
{
    public record Card(int Id, int[] Winners, int[] Numbers)
    {
        public int Repeats { get; set; } = 1;
    }

    public Day4() : base(2023, 4)
    {
    }

    protected override object DoPart1(Card[] input) =>
        input.Sum(card => Pow2(card.Numbers.Count(n => card.Winners.Contains(n))));

    private static int Pow2(int val) =>
        val switch
        {
            0 => 0,
            1 => 1,
            _ => (int) Math.Pow(2, val - 1)
        };

    protected override object DoPart2(Card[] input)
    {
        var totalCards = 0;

        foreach (var card in input)
        {
            while (card.Repeats-- > 0)
            {
                totalCards++;
                
                var score = card.Numbers.Count(n => card.Winners.Contains(n));

                for (var i = card.Id; i < card.Id + score; i++)
                    input[i].Repeats++;
            }
        }

        return totalCards;
    }

    protected override Card[] ParseInput(string input)
    {
        return input.Split("\n")
            .Select(line => Regex.Match(line, @"Card\s+(?<card>\d+):(?<winners>[\s\d]+)\|(?<numbers>[\s\d]+)"))
            .Select(match => new Card(
                match.Groups["card"].Value.ToInt(),
                match.Groups["winners"].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(v => v.ToInt()).ToArray(),
                match.Groups["numbers"].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(v => v.ToInt()).ToArray()
            ))
            .ToArray();
    }
}