using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day1 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1() => 
        Input.Sum(Convert.ToInt32);

    public override object ExecutePart2()
    {
        var recentFrequencies = new SortedSet<int> {0};
        var frequency = 0;

        while (true)
        {
            foreach (var change in Input)
            {
                frequency += Convert.ToInt32(change);

                if (recentFrequencies.Contains(frequency))
                    return frequency;

                recentFrequencies.Add(frequency);
            }
        }
    }
}