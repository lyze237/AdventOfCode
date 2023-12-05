using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day5 : Day<(ulong[] seeds, Day5.Map[] maps)>
{
    public record Map(string From, string To)
    {
        public List<Mapping> Mapping { get; init; } = new();
    }

    public record Mapping(ulong DestRangeStart, ulong SourceRangeStart, ulong RangeLength);

    public Day5() : base(2023, 5)
    {
    }

    protected override object DoPart1((ulong[] seeds, Map[] maps) input) => 
        input.seeds.Aggregate(ulong.MaxValue, (current, seed) => Math.Min(current, CurrentItem(input, seed)));

    protected override object? DoPart2((ulong[] seeds, Map[] maps) input)
    {
        var ranges = new List<(ulong from, ulong to)>();
        for (var i = 0; i < input.seeds.Length; i += 2)
            ranges.Add((input.seeds[i], input.seeds[i] + input.seeds[i + 1]));

        var results = new ConcurrentBag<ulong>();

        Task.WhenAll(ranges.Select(seed => Task.Run(() =>
        {
            var minimumLocation = ulong.MaxValue;

            for (var i = seed.from; i < seed.to; i++)
                minimumLocation = Math.Min(minimumLocation, CurrentItem(input, i));

            results.Add(minimumLocation);
        })).ToArray()).Wait();

        return results.Min();
    }

    private static ulong CurrentItem((ulong[] seeds, Map[] maps) input, ulong seed)
    {
        var currentItem = seed;

        foreach (var map in input.maps)
        {
            foreach (var (destRangeStart, sourceRangeStart, rangeLength) in map.Mapping)
            {
                if (currentItem >= sourceRangeStart && currentItem <= sourceRangeStart + rangeLength)
                {
                    currentItem = destRangeStart + (currentItem - sourceRangeStart);
                    break;
                }
            }
        }

        return currentItem;
    }

    protected override (ulong[] seeds, Map[] maps) ParseInput(string input)
    {
        var lines = input.Split("\n");
        var seeds = Regex.Matches(lines[0], @"\d+").Select(s => s.Value.ToULong()).ToArray();

        var maps = new List<Map>();
        Map? currentMap = null;
        foreach (var line in lines)
        {
            var match = Regex.Match(line, @"(?<from>\w+)-to-(?<to>\w+) map:");
            if (maps.Count == 0 && !match.Success)
                continue;

            if (match.Success)
            {
                maps.Add(currentMap = new Map(match.Groups["from"].Value, match.Groups["to"].Value));
                continue;
            }

            if (string.IsNullOrWhiteSpace(line))
                continue;

            var (from, to, range, _) = line.Split(" ");
            currentMap!.Mapping.Add(new Mapping(from!.ToULong(), to!.ToULong(), range!.ToULong()));
        }

        return (seeds, maps.ToArray());
    }
}