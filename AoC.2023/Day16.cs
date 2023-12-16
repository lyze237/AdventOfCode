using System.Collections.Concurrent;
using System.Xml;
using AoC.Framework;
using AoC.Framework.Data;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace AoC._2023;

[TestFixture]
public class Day16 : Day<char[][]>
{
    public Day16() : base(2023, 16, true)
    {
    }

    protected override object DoPart1(char[][] input) => 
        ShootBeam(input, new Point(0, 0), Point.Right).Count;

    protected override object DoPart2(char[][] input)
    {
        var outcomes = new ConcurrentBag<int>();
        
        for (var y = 0; y < input.Length; y++)
        {
            outcomes.Add(ShootBeam(input, new Point(0, y), Point.Right).Count);
            outcomes.Add(ShootBeam(input, new Point(input[y].Length - 1, y), Point.Left).Count);
        }
        for (var x = 0; x < input[0].Length; x++)
        {
            outcomes.Add(ShootBeam(input, new Point(x, 0), Point.Down).Count);
            outcomes.Add(ShootBeam(input, new Point(x, input.Length - 1), Point.Up).Count);
        }

        return outcomes.Max();
    }
    
    private Dictionary<Point, List<Point>> ShootBeam(char[][] input, Point point, Point direction) => 
        ShootBeam(input, point, direction, new Dictionary<Point, List<Point>>());

    private static Dictionary<Point, List<Point>> ShootBeam(char[][] input, Point point, Point direction, Dictionary<Point, List<Point>> energized)
    {
        while (point is { X: >= 0, Y: >= 0 } && point.X < input[0].Length && point.Y < input.Length)
        {
            if (!energized.ContainsKey(point))
                energized.Add(point, new List<Point>());
            if (energized[point].Contains(direction))
                return energized;
            
            energized[point].Add(direction);
            
            switch (input[point.Y][point.X])
            {
                case '.':
                    point += direction;
                    break;

                case '/' when direction == Point.Right:
                    point += direction = Point.Up;
                    break;
                case '/' when direction == Point.Left:
                    point += direction = Point.Down;
                    break;
                case '/' when direction == Point.Down:
                    point += direction = Point.Left;
                    break;
                case '/' when direction == Point.Up:
                    point += direction = Point.Right;
                    break;

                case '\\' when direction == Point.Right:
                    point += direction = Point.Down;
                    break;
                case '\\' when direction == Point.Left:
                    point += direction = Point.Up;
                    break;
                case '\\' when direction == Point.Down:
                    point += direction = Point.Right;
                    break;
                case '\\' when direction == Point.Up:
                    point += direction = Point.Left;
                    break;

                case '-' when direction == Point.Right || direction == Point.Left:
                    point += direction;
                    break;
                case '|' when direction == Point.Up || direction == Point.Down:
                    point += direction;
                    break;

                case '-' when direction == Point.Up || direction == Point.Down:
                    energized = ShootBeam(input, point + Point.Right, Point.Right, energized);
                    point += direction = Point.Left;
                    break;
                case '|' when direction == Point.Left || direction == Point.Right:
                    energized = ShootBeam(input, point + Point.Down, Point.Down, energized);
                    point += direction = Point.Up;
                    break;
            }
        }

        return energized;
    }

    protected override char[][] ParseInput(string input) =>
        input.Split("\n").Select(line => line.ToCharArray()).ToArray();
}