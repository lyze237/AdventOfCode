namespace AoC.Framework.Extensions;

public static class EnumerableExtensions
{
    public static int Mul(this IEnumerable<int> source) =>
        source.Aggregate((left, right) => left * right);

    public static IEnumerable<ulong> CreateRange(ulong start, ulong to)
    {
        while (start < to)
            yield return start++;
    }

    public static IEnumerable<T> RepeatIndefinitely<T>(this IEnumerable<T> source)
    {
        var enumerable = source as T[] ?? source.ToArray();

        while (true)
            foreach (var item in enumerable)
                yield return item;
    }

    public static long FindLcm(this IEnumerable<int> source) => 
        source.Aggregate(1L, (l, i) => FindLcm(l, i));
    
    public static long FindLcm(this IEnumerable<long> source) => 
        source.Aggregate(1L, FindLcm);

    private static long FindGcd(long a, long b)
    {
        if (a == 0 || b == 0) return Math.Max(a, b);
        return (a % b == 0) ? b : FindGcd(b, a % b);
    }

    private static long FindLcm(long a, long b) => a * b / FindGcd(a, b);
}