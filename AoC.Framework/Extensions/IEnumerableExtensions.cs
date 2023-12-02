namespace AoC.Framework.Extensions;

public static class EnumerableExtensions
{
    public static int Mul(this IEnumerable<int> source) => 
        source.Aggregate((left, right) => left * right);
}