using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2022;

[TestFixture]
public class Day14 : Day<HashSet<Day14.Point>>
{
    public record Point(int X, int Y);
    
    public Day14() : base(2022, 14) { }

    protected override object DoPart1(HashSet<Point> input)
    {
        var spawnedSands = 0;
        for (; spawnedSands < 10000; spawnedSands++)
        {
            var sand = SummonSand(input, new Point(500, 0));
            if (sand == null)
                break;

            input.Add(sand);
        }

        return spawnedSands;
    }

    protected override object DoPart2(HashSet<Point> input)
    {
        GenerateFloor(input);

        var spawnedSands = 0;
        for (; !input.Contains(new Point(500, 0)); spawnedSands++)
        {
            var sand = SummonSand(input, new Point(500, 0));
            if (sand == null)
                break;

            input.Add(sand);
        }

        return spawnedSands;
    }

    private static void GenerateFloor(HashSet<Point> map)
    {
        var floorY = 0;

        foreach (var (_, y) in map)
            floorY = Math.Max(floorY, y);

        for (var floorX = -10000; floorX < 10000; floorX++)
            map.Add(new Point(floorX, floorY + 2));
    }

    private static Point? SummonSand(HashSet<Point> map, Point start)
    {
        (Point position, bool rested) sand = (start, false);

        for (var i = 0; i < 10000 && sand.rested == false; i++)
            sand = ProcessSandTick(map, sand.position);

        return !sand.rested ? null : sand.position;
    }

    private static (Point position, bool rest) ProcessSandTick(HashSet<Point> map, Point position)
    {
        if (!map.Contains(position with { Y = position.Y + 1 }))
            return (position with { Y = position.Y + 1 }, false);

        if (!map.Contains(new Point(position.X - 1, position.Y + 1)))
            return (new Point(position.X - 1, position.Y + 1), false);

        if (!map.Contains(new Point(position.X + 1, position.Y + 1)))
            return (new Point(position.X + 1, position.Y + 1), false);

        return (position, true);
    }

    protected override HashSet<Point> ParseInput(string input)
    {
        var map = new HashSet<Point>();

        input.Split("\n").ToList().ForEach(line => FillLineInMap(map, line));

        return map;
    }


    private static void FillLineInMap(HashSet<Point> map, string line)
    {
        var points = new List<Point>();

        foreach (var point in line.Split(" -> "))
        {
            var (x, y, _) = point.Split(",").ToInts();
            points.Add(new Point(x!, y!));
        }

        for (var i = 1; i < points.Count; i++)
        {
            var (start, end) = (points[i - 1], points[i]);

            while (start != end)
            {
                map.Add(start);

                if (start.X < end.X)
                    start = start with { X = start.X + 1 };
                else if (start.X > end.X)
                    start = start with { X = start.X - 1 };
                else if (start.Y > end.Y)
                    start = start with { Y = start.Y - 1 };
                else if (start.Y < end.Y)
                    start = start with { Y = start.Y + 1 };
            }

            map.Add(start);
        }
    }
}