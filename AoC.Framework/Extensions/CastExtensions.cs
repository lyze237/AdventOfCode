namespace AoC.Framework.Extensions;

public static class CastExtensions
{
    public static int ToInt(this string s) =>
        Convert.ToInt32(s);
    
    public static long ToLong(this string s) =>
        Convert.ToInt64(s);
}