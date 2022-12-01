using System.Drawing;
using System.Text.RegularExpressions;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day10 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var stars = Input.Select(line => new Star(line)).ToList();

        var topLeft = new Point(stars.Min(s => s.Position.X), stars.Min(s => s.Position.Y));
        var bottomRight = new Point(stars.Max(s => s.Position.X), stars.Max(s => s.Position.Y));

        while (true)
        {
            var oldStars = stars.ToList();

            stars.ForEach(s => s.Update());

            var newTopLeft = new Point(stars.Min(s => s.Position.X), stars.Min(s => s.Position.Y));
            var newBottomRight = new Point(stars.Max(s => s.Position.X), stars.Max(s => s.Position.Y));

            if (Math.Abs(newBottomRight.X - newTopLeft.X) > Math.Abs(bottomRight.X - topLeft.X) ||
                Math.Abs(newBottomRight.Y - newTopLeft.Y) > Math.Abs(bottomRight.Y - topLeft.Y))
            {
                stars.ForEach(s => s.Update(false));
                // print
                var toPrint = "";

                for (var y = topLeft.Y; y <= bottomRight.Y; y++)
                {
                    for (var x = topLeft.X; x <= bottomRight.X; x++)

                    {
                        toPrint += oldStars.Any(s => s.Position.X == x && s.Position.Y == y) ? '#' : '.';
                    }

                    toPrint += "\r\n";
                }

                return toPrint;
            }

            topLeft = newTopLeft;
            bottomRight = newBottomRight;
        }
    }

    public override object ExecutePart2()
    {
        var stars = Input.Select(line => new Star(line)).ToList();

        var topLeft = new Point(stars.Min(s => s.Position.X), stars.Min(s => s.Position.Y));
        var bottomRight = new Point(stars.Max(s => s.Position.X), stars.Max(s => s.Position.Y));

        var seconds = 0;

        while (true)
        {
            var oldStars = stars.ToList();

            stars.ForEach(s => s.Update());

            var newTopLeft = new Point(stars.Min(s => s.Position.X), stars.Min(s => s.Position.Y));
            var newBottomRight = new Point(stars.Max(s => s.Position.X), stars.Max(s => s.Position.Y));

            if (Math.Abs(newBottomRight.X - newTopLeft.X) > Math.Abs(bottomRight.X - topLeft.X) ||
                Math.Abs(newBottomRight.Y - newTopLeft.Y) > Math.Abs(bottomRight.Y - topLeft.Y))
            {
                stars.ForEach(s => s.Update(false));

                return seconds;
            }

            topLeft = newTopLeft;
            bottomRight = newBottomRight;

            seconds++;
        }
    }

    public class Star
    {
        public Point Position { get; set; }
        public Point Velocity { get; set; }

        public Star(string line)
        {
            var match = Regex.Match(line,
                @"position=<(?<posX>[- \d]+), (?<posY>[- \d]+)> velocity=<(?<velX>[- \d]+), (?<velY>[- \d]+)>");

            Position = new Point(Convert.ToInt32(match.Groups["posX"].Value),
                Convert.ToInt32(match.Groups["posY"].Value));
            Velocity = new Point(Convert.ToInt32(match.Groups["velX"].Value),
                Convert.ToInt32(match.Groups["velY"].Value));
        }

        public void Update(bool forward = true) => Position = forward
            ? new Point(Position.X + Velocity.X, Position.Y + Velocity.Y)
            : new Point(Position.X - Velocity.X, Position.Y - Velocity.Y);

        public override string ToString()
        {
            return $"{nameof(Position)}: {Position}, {nameof(Velocity)}: {Velocity}";
        }
    }
}