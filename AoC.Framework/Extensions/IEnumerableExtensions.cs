namespace AoC.Framework.Extensions;

public static class EnumerableExtensions
{
    public static int Mul(this IEnumerable<int> source) => 
        source.Aggregate((left, right) => left * right);
    
    public static IEnumerable<ulong> CreateRange(ulong start, ulong count)
    {
        var limit = start + count;

        while (start < limit)
        {
            yield return start;
            start++;
        }
    } 
}