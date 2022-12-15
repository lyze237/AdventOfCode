using System.Text.RegularExpressions;
using AoC.Framework;
using AoC.Framework.Extensions;

namespace AoC._2022;

public class Day15 : Day<List<Day15.SensorNBeacon>>
{
    public record Point(int X, int Y);

    public Day15() : base(2022, 15, true) { }

    protected override object DoPart1(List<SensorNBeacon> input)
    {
        const int row = 2_000_000;
        var signals = new HashSet<Point>();

        foreach (var sensorNBeacon in input)
        {
            var (sensor, distance) = (sensorNBeacon.Sensor, sensorNBeacon.Distance);
            var pointOnRow = sensor with { Y = row };
            var distanceOnRow = sensorNBeacon.Manhattan(pointOnRow);

            if (!sensorNBeacon.IsInRange(pointOnRow))
                continue;

            for (var x = sensor.X - (distance - distanceOnRow); x < sensor.X + (distance - distanceOnRow); x++)
                signals.Add(new Point(x, row));
        }

        return signals.Count;
    }

    protected override object DoPart2(List<SensorNBeacon> input)
    {
        const int highestPoint = 4_000_000;

        foreach (var sensorNBeacon in input)
        {
            var (sensor, distance) = (sensorNBeacon.Sensor, sensorNBeacon.Distance);

            for (var x = Math.Max(0, sensor.X - distance); x <= Math.Min(highestPoint, sensor.X + distance); ++x)
            {
                var topY = Math.Max(sensor.Y - (distance - Math.Abs(sensor.X - x)), 0);
                var bottomY = Math.Min(sensor.Y + distance - Math.Abs(sensor.X - x), highestPoint);

                var (topNotOk, bottomNotOk) = (false, false);
                foreach (var otherSensorNBeacon in input)
                {
                    topNotOk |= otherSensorNBeacon.Manhattan(new Point(x, topY)) < otherSensorNBeacon.Distance;
                    bottomNotOk |= otherSensorNBeacon.Manhattan(new Point(x, bottomY)) < otherSensorNBeacon.Distance;
                }

                if (!topNotOk || !bottomNotOk)
                    return x * highestPoint + (!topNotOk ? topY : bottomY);
            }
        }

        throw new ArgumentException("No beacon found");
    }

    protected override List<SensorNBeacon> ParseInput(string input)
    {
        var regex = new Regex(@"[-]*\d+");

        return input.Split("\n")
            .Select(line => regex.Matches(line).Select(r => r.Value.ToInt()).ToArray())
            .Select(r => new SensorNBeacon(new Point(r[0], r[1]), new Point(r[2], r[3]))).ToList();
    }

    public record SensorNBeacon(Point Sensor, Point Beacon)
    {
        public int Distance => Manhattan(Sensor, Beacon);

        public bool IsInRange(Point point) => 
            Manhattan(point) <= Distance;

        public int Manhattan(Point point) =>
            Manhattan(point, Sensor);
        
        private static int Manhattan(Point p1, Point p2) => 
            Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
    }
}