using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Day10
{
    public class Star
    {
        public Point Position { get; set; }
        public Point Velocity { get; set; }

        public Star(string line)
        {
            var match = Regex.Match(line, @"position=<(?<posX>[- \d]+), (?<posY>[- \d]+)> velocity=<(?<velX>[- \d]+), (?<velY>[- \d]+)>");
            
            Position = new Point(Convert.ToInt32(match.Groups["posX"].Value), Convert.ToInt32(match.Groups["posY"].Value));
            Velocity = new Point(Convert.ToInt32(match.Groups["velX"].Value), Convert.ToInt32(match.Groups["velY"].Value));
        }

        public void Update(bool forward = true) => Position = forward ? new Point(Position.X + Velocity.X, Position.Y + Velocity.Y) : new Point(Position.X - Velocity.X, Position.Y - Velocity.Y);

        public override string ToString()
        {
            return $"{nameof(Position)}: {Position}, {nameof(Velocity)}: {Velocity}";
        }
    }
}