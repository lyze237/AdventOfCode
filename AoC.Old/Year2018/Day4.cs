using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day4 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var lines = Input.OrderBy(l => l).ToList();

        var times = new Dictionary<int, Dictionary<int, int>>();

        var guard = 0;
        var at = 0;

        foreach (var line in lines)
        {
            if (line.Contains("falls asleep"))
            {
                at = Convert.ToInt32(line.Split(":")[1].Split("]")[0]);
            }
            else if (line.Contains("wakes up"))
            {
                var wakeup = Convert.ToInt32(line.Split(":")[1].Split("]")[0]);
                if (!times.ContainsKey(guard))
                    times.Add(guard, new Dictionary<int, int>());

                for (var min = at; min < wakeup; min++)
                {
                    if (!times[guard].ContainsKey(min))
                        times[guard].Add(min, 0);

                    times[guard][min]++;
                }
            }
            else
            {
                guard = Convert.ToInt32(line.Split("#")[1].Split(" ")[0]);
            }
        }


        var sleepy = times
            .ToDictionary(t => t.Key, t => t.Value.Sum(tv => tv.Value)).MaxBy(t => t.Value).Key;

        var sleepyAt = times[sleepy].MaxBy(t => t.Value).Key;

        return sleepy * sleepyAt;
    }

    public override object ExecutePart2()
    {
        var lines = Input.OrderBy(l => l).ToList();

        var times = new Dictionary<int, Dictionary<int, int>>();

        var guard = 0;
        var at = 0;

        foreach (var line in lines)
        {
            if (line.Contains("falls asleep"))
            {
                at = Convert.ToInt32(line.Split(":")[1].Split("]")[0]);
            }
            else if (line.Contains("wakes up"))
            {
                var wakeup = Convert.ToInt32(line.Split(":")[1].Split("]")[0]);
                if (!times.ContainsKey(guard))
                    times.Add(guard, new Dictionary<int, int>());

                for (var min = at; min < wakeup; min++)
                {
                    if (!times[guard].ContainsKey(min))
                        times[guard].Add(min, 0);

                    times[guard][min]++;
                }
            }
            else
            {
                guard = Convert.ToInt32(line.Split("#")[1].Split(" ")[0]);
            }
        }

        var sleepy = times
            .ToDictionary(t => t.Key, t => t.Value.MaxBy(tv => tv.Value)).MaxBy(t => t.Value.Value).Key;

        var sleepyAt = times[sleepy].MaxBy(t => t.Value).Key;

        return sleepy * sleepyAt;
    }
}