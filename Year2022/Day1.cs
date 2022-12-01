using Tidy.AdventOfCode;

namespace AdventOfCode.Year2022;

public class Day1 : Day
{
    public override object ExecutePart1()
    {
        var output = Input
                .Split("\n\n")
                .Select(g => g.Split("\n"))
                .Select(g => g.Sum(Convert.ToInt64))
                .OrderByDescending(g => g)
                .ToList();

            return output[0];
    }
   
    public override object ExecutePart2()
    {
        var output = Input
            .Split("\n\n")
            .Select(g => g.Split("\n"))
            .Select(g => g.Sum(Convert.ToInt64))
            .OrderByDescending(g => g)
            .ToList();

        return output[0] + output[1] + output[2];
    }
}