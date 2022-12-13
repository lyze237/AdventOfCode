using System.Drawing;
using AdventOfCode.Parsers;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day3 : Day<int>.WithParser<IntParser>
{
    public override object ExecutePart1()
    {
        var ring = 0;
        for (var maxRingVal = 1; maxRingVal < Input; maxRingVal += 8 * ++ring)
        {
        }

        Console.WriteLine("Ring: " + ring);
        var sequenceIndex = (Input - 1) % (2 * ring);
        var distAlongEdge = Math.Abs(sequenceIndex - ring);
        var taxiDist = distAlongEdge + ring;

        return taxiDist;
    }

    public override object ExecutePart2()
    {
        var grid = new Dictionary<Point, int>
        {
            [new Point(0, 0)] = 1
        };

        var direction = Direction.Right;
        var x = 0;
        var y = 0;

        var init = false;
        while (true)
        {
            var point = new Point(x, y);


            var val = 0;

            if (!init)
                init = true;
            else
                switch (direction)
                {
                    case Direction.Right:
                        if (!grid.ContainsKey(new Point(x, y + 1)))
                        {
                            direction = Direction.Up;
                        }

                        break;
                    case Direction.Left:
                        if (!grid.ContainsKey(new Point(x, y - 1)))
                        {
                            direction = Direction.Down;
                        }

                        break;
                    case Direction.Up:
                        if (!grid.ContainsKey(new Point(x - 1, y)))
                        {
                            direction = Direction.Left;
                        }

                        break;
                    case Direction.Down:
                        if (!grid.ContainsKey(new Point(x + 1, y)))
                        {
                            direction = Direction.Right;
                        }

                        break;
                }


            if (grid.ContainsKey(new Point(x - 1, y - 1)))
            {
                val += grid[new Point(x - 1, y - 1)];
            }

            if (grid.ContainsKey(new Point(x, y - 1)))
            {
                val += grid[new Point(x, y - 1)];
            }

            if (grid.ContainsKey(new Point(x + 1, y - 1)))
            {
                val += grid[new Point(x + 1, y - 1)];
            }


            if (grid.ContainsKey(new Point(x - 1, y)))
            {
                val += grid[new Point(x - 1, y)];
            }

            if (grid.ContainsKey(new Point(x, y)))
            {
                val += grid[new Point(x, y)];
            }

            if (grid.ContainsKey(new Point(x + 1, y)))
            {
                val += grid[new Point(x + 1, y)];
            }


            if (grid.ContainsKey(new Point(x - 1, y + 1)))
            {
                val += grid[new Point(x - 1, y + 1)];
            }

            if (grid.ContainsKey(new Point(x, y + 1)))
            {
                val += grid[new Point(x, y + 1)];
            }

            if (grid.ContainsKey(new Point(x + 1, y + 1)))
            {
                val += grid[new Point(x + 1, y + 1)];
            }

            grid[point] = val;
            Console.WriteLine(point + ": " + val + " (" + grid.Count + ")");

            if (val > Input)
            {
                return val;
            }


            switch (direction)
            {
                case Direction.Right:
                    x++;
                    break;
                case Direction.Left:
                    x--;
                    break;
                case Direction.Up:
                    y++;
                    break;
                case Direction.Down:
                    y--;
                    break;
            }
        }
    }

    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}