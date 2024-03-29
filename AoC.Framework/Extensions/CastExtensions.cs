﻿namespace AoC.Framework.Extensions;

public static class CastExtensions
{
    public static int ToInt(this char c) =>
        c - '0';
    
    public static int ToInt(this string s) =>
        Convert.ToInt32(s);
    
    public static long ToLong(this string s) =>
        Convert.ToInt64(s);

    public static ulong ToULong(this string s) =>
        Convert.ToUInt64(s);
    
    public static int[] ToInts(this IEnumerable<string> ints) =>
        ints.Select(i => i.ToInt()).ToArray();
}