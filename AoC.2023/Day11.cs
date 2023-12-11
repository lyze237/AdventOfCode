using AoC.Framework;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day11 : Day<List<List<char>>>
{
    public record Star(long X, long Y)
    {
        private long Steps { get; } = Math.Abs(X) + Math.Abs(Y);
        public long Distance(Star other) => new Star(other.X - X, other.Y - Y).Steps;
    }

    public Day11() : base(2023, 11, true)
    {
    }

    protected override object DoPart1(List<List<char>> input) => 
        Solve(input, 1);

    protected override object DoPart2(List<List<char>> input) => 
        Solve(input, 999999);

    private static object Solve(IReadOnlyList<List<char>> input, long expand)
    {
        var stars = FindStars(input, expand);

        var steps = 0L;
        for (var first = 0; first < stars.Count; first++)
            for (var second = first + 1; second < stars.Count; second++)
                steps += stars[first].Distance(stars[second]);

        return steps;
    }

    private static List<Star> FindStars(IReadOnlyList<List<char>> input, long expand)
    {
        var stars = new List<Star>();

        var linesToAdd = input.Select((line, i) => (index: i, noStar: line.All(c => c == '.'))).Where(tuple => tuple.noStar).Select(tuple => tuple.index).ToArray();
        var columnsToAdd = input[0].Select((_, i) => (index: i, noStar: input.All(l => l[i] == '.'))).Where(tuple => tuple.noStar).Select(tuple => tuple.index).ToArray();

        for (var y = 0; y < input.Count; y++)
        {
            var yOffset = linesToAdd.Count(l => l <= y) * expand;
            for (var x = 0; x < input[y].Count; x++)
            {
                if (input[y][x] == '#')
                {
                    var xOffset = columnsToAdd.Count(c => c <= x) * expand;
                    stars.Add(new Star(y + yOffset, x + xOffset));
                }
            }
        }

        return stars;
    }

    protected override List<List<char>> ParseInput(string input) =>
        input
            .Split('\n')
            .Select(line => line.ToCharArray().ToList())
            .ToList();
}