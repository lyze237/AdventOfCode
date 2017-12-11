using System.Linq;
using System;
using System.Drawing;
using Utils;

namespace Day11
{
    public class Day11Challenge2 : Challenge<int>
    {
        public Day11Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            Point origin = new Point(0, 0);
            Point coords = new Point(0, 0);
            var maxDistance = 0;
            
            foreach (var input in GetInputFile().Split(','))
            {
                coords = Move(input, coords);
                maxDistance = Math.Max(maxDistance, GetDistance(origin, coords));
            }

            return maxDistance;
        }
        
        private static Point Move(string d, Point position)
        {
            switch (d)
            {
                case "n":
                    return new Point(position.X, position.Y + 2);
                case "s":
                    return new Point(position.X, position.Y - 2);
                    
                case "ne":
                    return new Point(position.X + 1, position.Y + 1);
                case "nw":
                    return new Point(position.X - 1, position.Y + 1);
                    
                case "se":
                    return new Point(position.X + 1, position.Y - 1);
                case "sw":
                    return new Point(position.X - 1, position.Y - 1);
                    
                default:
                    throw new ArgumentException(d);
            }
        }
        
        private static int GetDistance(Point origin, Point position)
        {
            var xM = Math.Abs(position.X - origin.X);
            var yM = (Math.Abs(position.Y - origin.Y) - xM) / 2;

            return xM + yM;
        }
    }
}