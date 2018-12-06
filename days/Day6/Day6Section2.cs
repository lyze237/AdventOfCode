using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using AdventOfCodeLibrary.days;

namespace Day6
{
    public class Day6Section2 : ProgressDay
    {
        public class BetterPoint
        {
            public Point Point { get; set; }
            public int Id { get; set; }

            public BetterPoint(Point point, int id)
            {
                Point = point;
                Id = id;
            }

            public override string ToString()
            {
                return $"{nameof(Point)}: {Point}, {nameof(Id)}: {Id}";
            }

            protected bool Equals(BetterPoint other)
            {
                return Point.Equals(other.Point) && Id == other.Id;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((BetterPoint) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (Point.GetHashCode() * 397) ^ Id;
                }
            }
        }

        public Day6Section2() : base(6, 2)
        {
        }

        protected override object RunInternal(string input)
        {
            const int maxDistance = 10000;

            var points = input.Split("\r\n").Select(s => s.Split(", "))
                .Select((s, i) =>
                    new BetterPoint(new Point(
                        Convert.ToInt32(s[0]),
                        Convert.ToInt32(s[1])
                    ), i)
                ).ToList();

            var gridSize = points.OrderByDescending(p => p.Point.X).ThenByDescending(p => p.Point.Y).First();

            var grid = new int[gridSize.Point.X + 1, gridSize.Point.Y + 1];

            var count = 0;

            ProgressBar.MaxValue = grid.Length;
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                for (var y = 0; y < grid.GetLength(1); y++)
                {
                    var distances = CalculateDistances(points, x, y);
                    int sum = distances.Sum(d => d.Value);
                    if (sum < maxDistance)
                        count++;

                    ProgressBar.Value++;
                }
            }

            return count;
        }

        private Dictionary<BetterPoint, int> CalculateDistances(List<BetterPoint> points, int x, int y)
        {
            var ret = new Dictionary<BetterPoint, int>();
            points.ForEach(p => ret.Add(p, CalculateDistance(p, x, y)));
            return ret.OrderBy(d => d.Value).ToDictionary(d => d.Key, d => d.Value);
        }

        private int CalculateDistance(BetterPoint position, int x, int y)
        {
            return Math.Abs(x - position.Point.X) + Math.Abs(y - position.Point.Y);
        }
    }
}