using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day7 : Day<List<Day7.Hand>>
{
    public record Hand(int[] Cards, int Bid, bool Joker) : IComparable<Hand>
    {
        private enum Hands
        {
            HighCard = 1,
            OnePair = 2,
            TwoPair = 3,
            ThreeOfAKind = 4,
            FullHouse = 5,
            FourOfAKind = 6,
            FiveOfAKind = 7
        }
        
        public int CompareTo(Hand? other)
        {
            if (other == null)
                throw new ArgumentException("Other is null");

            var score = GetScore();
            var otherScore = other.GetScore();

            return score == otherScore
                ? Cards.Select((t, i) => t.CompareTo(other.Cards[i])).FirstOrDefault(compare => compare != 0)
                : score.CompareTo(otherScore);
        }

        private Hands GetScore()
        {
            var sources = Cards
                .GroupBy(c => c)
                .Select(c => (face: c.Key, count: c.Count()))
                .OrderByDescending(c => c.count).ToList();

            var hasJoker = Joker && sources.Any(c => c.face == 0);
            
            switch (sources[0].count)
            {
                case 5: return Hands.FiveOfAKind;
                case 4: return hasJoker ? Hands.FiveOfAKind : Hands.FourOfAKind;
                case 3:
                    if (sources[1].count != 2) 
                        return hasJoker ? Hands.FourOfAKind : Hands.ThreeOfAKind;
                    
                    if (Joker && (sources[0].face == 0 || sources[1].face == 0))
                        return Hands.FiveOfAKind;
                    return Hands.FullHouse;
                case 2:
                    if (sources[1].count != 2) 
                        return hasJoker ? Hands.ThreeOfAKind : Hands.OnePair;

                    return Joker switch
                    {
                        true when sources[0].face == 0 || sources[1].face == 0 => Hands.FourOfAKind,
                        true when sources[2].face == 0 => Hands.FullHouse,
                        _ => Hands.TwoPair
                    };

                default:
                    return hasJoker ? Hands.OnePair : Hands.HighCard;
            }
        }
    }

    public Day7() : base(2023, 7)
    {
    }

    protected override object DoPart1(List<Hand> input)
    {
        input.Sort();
        return input.Select((t, i) => t.Bid * (i + 1)).Sum();
    }

    protected override object DoPart2(List<Hand> input) =>
        DoPart1(input);

    protected override List<Hand> ParseInput(string input) =>
        ParseInput(input, false);

    protected override List<Hand> ParseInput2(string input) =>
        ParseInput(input, true);

    private static List<Hand> ParseInput(string input, bool joker) =>
        input.Split("\n")
            .Select(i =>
            {
                var (cards, bid, _) = i.Split(" ");
                return new Hand(cards!.Select(c => MapStrength(c, joker)).ToArray(), bid!.ToInt(), joker);
            }).ToList();

    private static int MapStrength(char c, bool joker)
    {
        var strengths = joker
            ? new[] { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' }
            : new[] { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };

        for (var i = 0; i < strengths.Length; i++)
            if (strengths[i] == c)
                return i;

        throw new ArgumentException($"Strength not found {c}");
    }
}