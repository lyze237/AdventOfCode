using AoC.Framework;
using AoC.Framework.Extensions;

namespace AoC._2024;

public class Day2() : Day<int[][]>(2024, 2, "2", "4")
{
    protected override object DoPart1(int[][] input) => 
        input.Count(CheckIfReportIsGood);

    protected override object DoPart2(int[][] input)
    {
        var safeReports = 0;
        foreach (var report in input)
        {
            for (var i = 0; i < report.Length; i++)
            {
                var modifiedReport = report.Where((_, index) => i != index).ToArray();
                if (CheckIfReportIsGood(modifiedReport))
                {
                    safeReports++;
                    break;
                }
            }
        }

        return safeReports;
    }

    private static bool CheckIfReportIsGood(int[] levels)
    {
        var increasing = CheckIfIsIncreasing(levels);
        if (increasing == null)
            return false;

        var isGoodReport = true;
            
        for (var i = 1; i < levels.Length; i++)
        {
            var last = levels[i - 1];
            var current = levels[i];

            if ((increasing.Value && IsBetween(current, last + 1, last + 3)) || (!increasing.Value && IsBetween(current, last - 3, last - 1)))
                continue;

            isGoodReport = false;
            break;
        }

        return isGoodReport;
    }

    private static bool? CheckIfIsIncreasing(int[] report)
    {
        var first = report[0];
        var second = report[1];
        
        if (first < second)
            return true;

        if (first > second)
            return false;

        return null;
    }

    private static bool IsBetween(int value, int min, int max) =>
        value >= min && value <= max;
    
    protected override int[][] ParseInput(string input)
    {
        return input.Split("\n")
            .Select(l => l.Split(" ").Select(i => i.ToInt()).ToArray())
            .ToArray();
    }
}