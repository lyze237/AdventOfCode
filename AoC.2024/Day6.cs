using System.ComponentModel.Design;
using AoC.Framework;
using AoC.Framework.Data;
using AoC.Framework.Extensions;

namespace AoC._2024;

public class Day6() : Day<char[][]>(2024, 6, true)
{
    protected override object DoPart1(char[][] input)
    {
        var direction = Direction.Up;
        var guardPoint = FindGuard(input);

        var visited = new HashSet<Point> { guardPoint };

        while (true)
        {
            var newGuardPoint = guardPoint.Move(direction);
            if (!input.InRectangle(newGuardPoint))
            {
                Console.WriteLine(newGuardPoint);
                break;
            }

            if (input.Get(newGuardPoint) == '#')
                direction = direction.RotateRight90();
            else
                guardPoint = newGuardPoint;

            input[guardPoint.Y][guardPoint.X] = 'X';
            visited.Add(guardPoint);
        }

        input.Print();

        return visited.Count;
    }


    protected override object DoPart2(char[][] input)
    {
        var guardPoint = FindGuard(input);

        var loopCounts = 0;
        ParallelEnumerable.Range(0, input.Length)
            .ForAll(y =>
            {
                ParallelEnumerable.Range(0, input[y].Length)
                    .ForAll(x =>
                    {
                        if (IsLoop(input, guardPoint, Direction.Up, new Point(x, y)))
                            Interlocked.Increment(ref loopCounts);
                    });
            });

        return loopCounts;
    }

    private static bool IsLoop(char[][] input, Point guardPoint, Direction guardDirection, Point obstacle)
    {
        if (guardPoint == obstacle)
            return false;

        var visited = new HashSet<(Point, Direction)>();

        while (true)
        {
            var newGuardPoint = guardPoint.Move(guardDirection);
            if (!input.InRectangle(newGuardPoint))
                return false;

            if (newGuardPoint == obstacle || input.Get(newGuardPoint) == '#')
                guardDirection = guardDirection.RotateRight90();
            else
                guardPoint = newGuardPoint;

            if (!visited.Add((guardPoint, guardDirection)))
                return true;
        }
    }

    private static Point FindGuard(char[][] input)
    {
        for (var y = 0; y < input.Length; y++)
            for (var x = 0; x < input[y].Length; x++)
                if (input[y][x] == '^')
                    return new Point(x, y);

        throw new ArgumentException("Guard not found");
    }

    protected override char[][] ParseInput(string input) =>
        input
            .Split("\n")
            .Select(line => line.ToCharArray())
            .ToArray();
}