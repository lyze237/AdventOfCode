using System.Collections.Concurrent;
using AoC.Framework;
using AoC.Framework.Data;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day16 : Day<char[][]>
{
    public Day16() : base(2023, 16, "46", null)
    {
    }

    protected override object DoPart1(char[][] input) => 
        ShootBeam(input, new Point(0, 0), Direction.Right).Count;

    protected override object DoPart2(char[][] input)
    {
        var outcomes = new ConcurrentBag<int>();
        
        for (var y = 0; y < input.Length; y++)
        {
            outcomes.Add(ShootBeam(input, new Point(0, y), Direction.Right).Count);
            outcomes.Add(ShootBeam(input, new Point(input[y].Length - 1, y), Direction.Left).Count);
        }
        for (var x = 0; x < input[0].Length; x++)
        {
            outcomes.Add(ShootBeam(input, new Point(x, 0), Direction.Down).Count);
            outcomes.Add(ShootBeam(input, new Point(x, input.Length - 1), Direction.Up).Count);
        }

        return outcomes.Max();
    }
    
    private Dictionary<Point, List<Direction>> ShootBeam(char[][] input, Point point, Direction direction) => 
        ShootBeam(input, point, direction, new Dictionary<Point, List<Direction>>());

    private static Dictionary<Point, List<Direction>> ShootBeam(char[][] input, Point point, Direction direction, Dictionary<Point, List<Direction>> energized)
    {
        while (point is { X: >= 0, Y: >= 0 } && point.X < input[0].Length && point.Y < input.Length)
        {
            if (!energized.ContainsKey(point))
                energized.Add(point, new List<Direction>());
            if (energized[point].Contains(direction))
                return energized;
            
            energized[point].Add(direction);
            
            switch (input[point.Y][point.X])
            {
                case '.':
                    point += direction.ToPoint();
                    break;

                case '/' when direction == Direction.Right:
                    point += (direction = Direction.Up).ToPoint();
                    break;
                case '/' when direction == Direction.Left:
                    point += (direction = Direction.Down).ToPoint();
                    break;
                case '/' when direction == Direction.Down:
                    point += (direction = Direction.Left).ToPoint();
                    break;
                case '/' when direction == Direction.Up:
                    point += (direction = Direction.Right).ToPoint();
                    break;

                case '\\' when direction == Direction.Right:
                    point += (direction = Direction.Down).ToPoint();
                    break;
                case '\\' when direction == Direction.Left:
                    point += (direction = Direction.Up).ToPoint();
                    break;
                case '\\' when direction == Direction.Down:
                    point += (direction = Direction.Right).ToPoint();
                    break;
                case '\\' when direction == Direction.Up:
                    point += (direction = Direction.Left).ToPoint();
                    break;

                case '-' when direction is Direction.Right or Direction.Left:
                    point += direction.ToPoint();
                    break;
                case '|' when direction is Direction.Up or Direction.Down:
                    point += direction.ToPoint();
                    break;

                case '-' when direction is Direction.Up or Direction.Down:
                    energized = ShootBeam(input, point.Move(Direction.Right), Direction.Right, energized);
                    point += (direction = Direction.Left).ToPoint();
                    break;
                case '|' when direction is Direction.Left or Direction.Right:
                    energized = ShootBeam(input, point.Move(Direction.Down), Direction.Down, energized);
                    point += (direction = Direction.Up).ToPoint();
                    break;
            }
        }

        return energized;
    }

    protected override char[][] ParseInput(string input) =>
        input.Split("\n").Select(line => line.ToCharArray()).ToArray();
}