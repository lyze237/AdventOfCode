using AoC.Framework;
using NUnit.Framework;

namespace AoC._2022;

[TestFixture]
public class Day3 : Day
{
    public Day3() : base(2022, 3) { }

    protected override object DoPart1(string[] input)
    {
        var total = 0;

        foreach (var line in input)
        {
            var left = line[..(line.Length / 2)];
            var right = line[(line.Length / 2)..];

            total += left.Where(l => right.Contains(l))
                .Distinct().Select(ConvertToPriority)
                .Sum();
        }

        return total;
    }

    protected override object DoPart2(string[] input)
    {
        var total = 0;

        foreach (var group in GroupByCount(input, 3))
        {
            var first = group.First().Distinct().ToList();

            total += group.Skip(1).Select(o => o.Distinct())
                .SelectMany(o => o.Where(a => first.Contains(a)))
                .GroupBy(o => o).Where(o => o.Count() > 1)
                .Select(l => ConvertToPriority(l.Key)).Sum();
        }

        return total;
    }
    
    private static int ConvertToPriority(char c)
    {
        if (c is >= 'A' and <= 'Z')
            return c - 'A' + 27;

        return c - 'a' + 1;
    }
    
    private static IEnumerable<IGrouping<int, TSource>> GroupByCount<TSource>(IEnumerable<TSource> enumerable, int cnt)
    {
        return enumerable.Select((item, index) => (item, index))
            .GroupBy(item => item.index / cnt, item => item.item);
    }
}