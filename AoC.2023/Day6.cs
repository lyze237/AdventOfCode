using System.Text.RegularExpressions;
using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day6 : Day<Day6.Race[]>
{
    public record Race(ulong Duration, ulong Record);
    
    public Day6() : base(2023, 6)
    {
    }
    
    protected override object DoPart1(Race[] input) => 
        input.Select(race => CalculateRace(race.Duration, race.Record)).Mul();

    protected override object DoPart2(Race[] input)
    {
        var duration = string.Join("", input.Select(i => i.Duration)).ToULong();
        var record = string.Join("", input.Select(i => i.Record)).ToULong();

        return CalculateRace(duration, record);
    }
    
    private static int CalculateRace(ulong duration, ulong record)
    {
        var goodDistances = 0;

        for (ulong button = 0; button < duration; button++)
        {
            var speed = button * 1;
            var leftOverTime = duration - button;

            var totalDistance = speed * leftOverTime;
            if (totalDistance > record)
                goodDistances++;

            if (goodDistances > 0 && totalDistance <= record)
                break; // we done mate
        }

        return goodDistances;
    }

    protected override Race[] ParseInput(string input)
    {
        var (duration, record, _) = input.Split("\n");
        var durations = Regex.Matches(duration!, @"\d+").Select(t => t.Value.ToULong()).ToArray();
        var records = Regex.Matches(record!, @"\d+").Select(d => d.Value.ToULong()).ToArray();

        return durations.Zip(records).Select(d => new Race(d.First, d.Second)).ToArray();
    }
}