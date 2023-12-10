using AoC.Framework;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day10 : Day<Dictionary<Day10.Point, char>>
{
    public record Point(int X, int Y)
    {
        public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
        public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
        public static Point operator -(Point a) => new(-a.X, -a.Y);
        public static Point operator -(Point a, int b) => new(a.X - b, a.Y - b);
    }

    private static readonly Point Up = new(0, -1);
    private static readonly Point Down = new(0, 1);
    private static readonly Point Left = new(-1, 0);
    private static readonly Point Right = new(1, 0);

    private static readonly Point[] Directions = { Up, Down, Left, Right };

    private static readonly Dictionary<char, Point[]> PipeDirections = new()
    {
        { '|', new[] { Up, Down } },
        { '-', new[] { Left, Right } },
        { 'L', new[] { Up, Right } },
        { 'J', new[] { Up, Left } },
        { '7', new[] { Left, Down } },
        { 'F', new[] { Right, Down } },
        { '.', Array.Empty<Point>() },
        { 'S', Directions.ToArray() }
    };

    public Day10() : base(2023, 10, true)
    {
    }

    protected override object DoPart1(Dictionary<Point, char> input) =>
        ParseLoop(input).Count / 2;


    protected override object DoPart2(Dictionary<Point, char> input)
    {
        var loop = ParseLoop(input);

        return input
            .AsParallel()
            .Where(point => !loop.Contains(point.Key))
            .Count(point => IsPointInPolygon(loop, point.Key));
    }

    private static List<Point> ParseLoop(IReadOnlyDictionary<Point, char> input)
    {
        var position = input.First(i => i.Value == 'S').Key;
        var positions = new List<Point>();

        // check if the opposite direction is inside the pipes end we want to go to (=> "valid connector from current pos")
        var direction = Directions.First(newDirection =>
            PipeDirections[input[position + newDirection]].Contains(-newDirection));

        while (true)
        {
            positions.Add(position);
            position += direction;

            if (input[position] == 'S')
                break;

            direction = PipeDirections[input[position]].First(newDirection => newDirection != -direction);
        }

        return positions;
    }

    // https://github.com/libgdx/libgdx/blob/ddc75209f30c3f6aa23d8888604663b09784320a/gdx/src/com/badlogic/gdx/math/Intersector.java#L108
    private static bool IsPointInPolygon(List<Point> polygon, Point point)
    {
        var last = polygon.Last();
        float x = point.X, y = point.Y;
        var oddNodes = false;

        foreach (var vertex in polygon)
        {
            if ((vertex.Y < y && last.Y >= y) || (last.Y < y && vertex.Y >= y))
                if (vertex.X + (y - vertex.Y) / (last.Y - vertex.Y) * (last.X - vertex.X) < x)
                    oddNodes = !oddNodes;

            last = vertex;
        }

        return oddNodes;
    }

    protected override Dictionary<Point, char> ParseInput(string input)
    {
        var dict = new Dictionary<Point, char>();
        var lines = input.Split("\n");

        for (var y = 0; y < lines.Length; y++)
        for (var x = 0; x < lines[y].Length; x++)
            dict.Add(new Point(x, y), lines[y][x]);

        return dict;
    }
}