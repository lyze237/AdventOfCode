using System;
using System.Drawing;
using System.IO;
using System.Linq;
using AdventOfCodeLibrary.days;

namespace Day10
{
    public class Day10Section1 : TimeDay
    {
        public Day10Section1() : base(10, 1)
        {
        }

        protected override object RunInternal(string input)
        {
            var stars = input.Split("\r\n").Select(line => new Star(line)).ToList();

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
                    // print
                    string toPrint = "";

                    for (int y = topLeft.Y; y <= bottomRight.Y; y++)
                    {
                        for (int x = topLeft.X; x <= bottomRight.X; x++)

                        {
                            toPrint += oldStars.Any(s => s.Position.X == x && s.Position.Y == y) ? '#' : '.';
                        }

                        toPrint += "\r\n";
                    }

                    
                    var file = new FileInfo("times/OUTPUT_DAY10.txt");
                    File.WriteAllText(file.FullName, toPrint);

                    return $"See {file.FullName}";
                }

                topLeft = newTopLeft;
                bottomRight = newBottomRight;

                seconds++;
            }
        }
    }
}