using System.Drawing;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day6 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var points = Input.Select(s => s.Split(", "))
            .Select((s, i) =>
                new BetterPoint(new Point(
                    Convert.ToInt32(s[0]),
                    Convert.ToInt32(s[1])
                ), i)
            ).ToList();

        var gridSize = points.OrderByDescending(p => p.Point.X).ThenByDescending(p => p.Point.Y).First();

        var grid = new int[gridSize.Point.X + 1, gridSize.Point.Y + 1];

        for (var x = 0; x < grid.GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetLength(1); y++)
            {
                var distances = CalculateDistances(points, x, y);
                if (distances.First().Value == distances.Skip(1).First().Value)
                {
                    grid[x, y] = -1;
                }
                else
                {
                    grid[x, y] = distances.First().Key.Id;
                }
            }
        }

        var excludes = new HashSet<int>();

        for (var x = 0; x < grid.GetLength(0); x++)
        {
            excludes.Add(grid[x, 0]);
            excludes.Add(grid[x, gridSize.Point.Y]);
        }

        for (var y = 0; y < grid.GetLength(1); y++)
        {
            excludes.Add(grid[0, y]);
            excludes.Add(grid[gridSize.Point.X, y]);
        }

        var counted = new Dictionary<int, int>();
        foreach (var i in grid)
        {
            if (counted.ContainsKey(i))
                counted[i]++;
            else
                counted.Add(i, 1);
        }

        counted = counted.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

        KeyValuePair<int, int> current;
        using (var enumerator = counted.GetEnumerator())
        {
            do
            {
                current = enumerator.Current;
                enumerator.MoveNext();
            } while (excludes.Contains(current.Key));
        }

        return current.Value;
    }

    public override object ExecutePart2()
    {
        const int maxDistance = 10000;

        var points = Input.Select(s => s.Split(", "))
            .Select((s, i) =>
                new BetterPoint(new Point(
                    Convert.ToInt32(s[0]),
                    Convert.ToInt32(s[1])
                ), i)
            ).ToList();

        var gridSize = points.OrderByDescending(p => p.Point.X).ThenByDescending(p => p.Point.Y).First();

        var grid = new int[gridSize.Point.X + 1, gridSize.Point.Y + 1];

        var count = 0;

        for (var x = 0; x < grid.GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetLength(1); y++)
            {
                var distances = CalculateDistances(points, x, y);
                var sum = distances.Sum(d => d.Value);
                if (sum < maxDistance)
                    count++;
            }
        }

        return count;
    }

    private static Dictionary<BetterPoint, int> CalculateDistances(List<BetterPoint> points, int x, int y)
    {
        var ret = new Dictionary<BetterPoint, int>();
        points.ForEach(p => ret.Add(p, CalculateDistance(p, x, y)));
        return ret.OrderBy(d => d.Value).ToDictionary(d => d.Key, d => d.Value);
    }

    private static int CalculateDistance(BetterPoint position, int x, int y)
    {
        return Math.Abs(x - position.Point.X) + Math.Abs(y - position.Point.Y);
    }

    private class BetterPoint
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
            return Equals((BetterPoint)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Point.GetHashCode() * 397) ^ Id;
            }
        }
    }
}