namespace AoC.Framework.Extensions;

public static class ArrayExtensions
{
    public static void Deconstruct<T>(this IList<T?> list, out T? first, out IList<T?> rest) {
        first = list.Count > 0 ? list[0] : default(T); // or throw
        
        rest = list.Skip(1).ToList();
    }

    public static void Deconstruct<T>(this IList<T?> list, out T? first, out T? second, out IList<T?> rest) {
        first = list.Count > 0 ? list[0] : default(T); // or throw
        second = list.Count > 1 ? list[1] : default(T); // or throw
        
        rest = list.Skip(2).ToList();
    }
    
    public static void Deconstruct<T>(this IList<T?> list, out T? first, out T? second, out T? third, out IList<T?> rest) {
        first = list.Count > 0 ? list[0] : default(T); // or throw
        second = list.Count > 1 ? list[1] : default(T); // or throw
        third = list.Count > 2 ? list[2] : default(T); // or throw
        
        rest = list.Skip(3).ToList();
    }
    
    public static T[][] Rotate<T>(this T[][] arr)
    {
        var width = arr[0].Length;
        var depth = arr.Length;

        var result = new T[width][];
        for (var i = 0; i < width; i++)
            result[i] = new T[depth];

        for (var i = 0; i < depth; i++)
        {
            for (var j = 0; j < width; j++)
            {
                result[j][depth - i - 1] = arr[i][j];
            }
        }

        return result;
    }
}