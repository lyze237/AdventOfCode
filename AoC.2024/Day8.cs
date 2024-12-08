using AoC.Framework;
using AoC.Framework.Data;

namespace AoC._2024;

public class Day8() : Day<(char[][] map, Day8.Antenna[] antennas)>(2024, 8)
{
    public record Antenna(char Frequency, Point Position);

    protected override object DoPart1((char[][] map, Antenna[] antennas) input) =>
        CalculateAllAntinodes(input.map, input.antennas);

    protected override object DoPart2((char[][] map, Antenna[] antennas) input) => 
        CalculateAllAntinodes(input.map, input.antennas, false, int.MaxValue);

    private static int CalculateAllAntinodes(char[][] map, Antenna[] antennas, bool skipOtherAntenna = true, int depth = 1)
    {
        var antinodes = new HashSet<Point>();

        foreach (var antenna in antennas)
        {
            var otherAntennas = GetOtherAntennas(antenna, antennas);

            foreach (var (_, otherPosition) in otherAntennas)
                foreach (var antinodePoint in CalculateAntinodesForAntenna(antenna, otherPosition, map, skipOtherAntenna, depth))
                    antinodes.Add(antinodePoint);
        }

        return antinodes.Count;
    }

    private static Point[] CalculateAntinodesForAntenna(Antenna antenna, Point otherAntenna, char[][] map, bool skipOtherAntenna, int depth)
    {
        var antinodes = new HashSet<Point>();

        var distance = antenna.Position.XyDistance(otherAntenna);
        // part 1 skips antenna position
        var potentialAntinodePosition = skipOtherAntenna ? antenna.Position.Move(distance + distance) : antenna.Position.Move(distance);

        for (var i = 0; i < depth && potentialAntinodePosition.InRectangle(map); i++)
        {
            antinodes.Add(potentialAntinodePosition);
            potentialAntinodePosition = potentialAntinodePosition.Move(distance);
        }

        return antinodes.ToArray();
    }

    private static Antenna[] GetOtherAntennas(Antenna antenna, Antenna[] allAntennas)
    {
        return allAntennas
            .Where(a => a.Position != antenna.Position)
            .Where(a => a.Frequency == antenna.Frequency)
            .ToArray();
    }

    protected override (char[][] map, Antenna[] antennas) ParseInput(string input)
    {
        var antennas = new List<Antenna>();

        var map = input.Split("\n").Select(line => line.ToCharArray()).ToArray();
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[y].Length; x++)
            {
                var frequency = map[y][x];
                if (frequency != '.')
                    antennas.Add(new Antenna(frequency, new Point(x, y)));
            }
        }

        return (map, antennas.ToArray());
    }
}