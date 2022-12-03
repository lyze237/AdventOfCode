using Tidy.AdventOfCode;

namespace AdventOfCode.Year2022;

public class Day3 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var total = 0;

        foreach (var line in Input)
        {
            var left = line[..(line.Length / 2)];
            var right = line[(line.Length / 2)..];

            total += left.Where(l => right.Contains(l))
                .Distinct().Select(ConvertToPriority)
                .Sum();
        }

        return total;
    }

    public override object ExecutePart2()
    {
        var total = 0;

        foreach (var group in Input.GroupByCount(3))
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
}

public static class LinqExtensions
{
    public static IEnumerable<IGrouping<int, TSource>> GroupByCount<TSource>(this IEnumerable<TSource> enumerable, int cnt)
    {
        return enumerable.Select((item, index) => (item, index))
            .GroupBy(item => item.index / cnt, item => item.item);
    }
}