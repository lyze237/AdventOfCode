using System.Drawing;
using AdventOfCode.Parsers;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day11 : Day<string[]>.WithParser<CommaParser>
{
    public override object ExecutePart1()
    {
        var origin = new Point(0, 0);
        var coords = new Point(0, 0);

        coords = Input.Aggregate(coords, (current, input) => Move(input, current));

        return GetDistance(origin, coords);
    }

    public override object ExecutePart2()
    {
        var origin = new Point(0, 0);
        var coords = new Point(0, 0);
        var maxDistance = 0;
            
        foreach (var input in Input)
        {
            coords = Move(input, coords);
            maxDistance = Math.Max(maxDistance, GetDistance(origin, coords));
        }

        return maxDistance;
    }
    
    private static Point Move(string d, Point position)
    {
        return d switch
        {
            "n" => position with { Y = position.Y + 2 },
            "s" => position with { Y = position.Y - 2 },
            "ne" => new Point(position.X + 1, position.Y + 1),
            "nw" => new Point(position.X - 1, position.Y + 1),
            "se" => new Point(position.X + 1, position.Y - 1),
            "sw" => new Point(position.X - 1, position.Y - 1),
            _ => throw new ArgumentException(d)
        };
    }
        
    private static int GetDistance(Point origin, Point position)
    {
        var xM = Math.Abs(position.X - origin.X);
        var yM = (Math.Abs(position.Y - origin.Y) - xM) / 2;

        return xM + yM;
    }
}