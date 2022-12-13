using AdventOfCode.Parsers;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day10 : Day
{
    public override object ExecutePart1()
    {
        var lengths = Input.Split(",").Select(s => Convert.ToInt32(s)).ToArray();
        var tmpList = new int[256];
        for (var i = 0; i < tmpList.Length; i++)
        {
            tmpList[i] = i;
        }

        var list = new WrapArray<int>(tmpList);

        var cursor = 0;

        Console.WriteLine($"L: {string.Join(", ", lengths)}");

        for (var i = 0; i < lengths.Length; i++)
        {
            Console.WriteLine($"B {i}: {string.Join(", ", tmpList)}");

            for (var startIndex = cursor; startIndex < cursor + lengths[i] / 2; startIndex++)
            {
                (list[cursor + lengths[i] - 1 - (startIndex - cursor)], list[startIndex]) = (list[startIndex],
                    list[cursor + lengths[i] - 1 - (startIndex - cursor)]);
            }

            cursor += lengths[i] + i;
            Console.WriteLine($"A {i}: {string.Join(", ", tmpList)}\nC: {cursor % list.Length}\n");
        }

        return tmpList[0] * tmpList[1];
    }

    public override object ExecutePart2()
    {
        var lengths = Input.ToCharArray().Select(c => (int)c)
            .Append(17).Append(31).Append(73).Append(47).Append(23)
            .ToArray();
        var tmpList = new int[256];
        for (var i = 0; i < tmpList.Length; i++)
        {
            tmpList[i] = i;
        }

        var list = new WrapArray<int>(tmpList);

        var cursor = 0;
        var skipSize = 0;

        for (var z = 0; z < 64; z++)
        {
            for (var i = 0; i < lengths.Length; i++, skipSize++)
            {
                for (var startIndex = cursor; startIndex < cursor + lengths[i] / 2; startIndex++)
                {
                    (list[cursor + lengths[i] - 1 - (startIndex - cursor)], list[startIndex]) = (list[startIndex],
                        list[cursor + lengths[i] - 1 - (startIndex - cursor)]);
                }

                cursor += lengths[i] + skipSize;
            }
        }

        var xorList = new List<int>();
        for (var i = 0; i < list.Length; i += 16)
        {
            var toXor = list.Array.Skip(i).Take(16).ToArray();
            var xorResult = toXor[0];

            for (var j = 1; j < toXor.Length; j++)
            {
                xorResult ^= toXor[j];
            }

            xorList.Add(xorResult);
        }

        var ret = "";
        foreach (var element in xorList)
        {
            ret += element.ToString("X").PadLeft(2, '0');
        }

        return ret.ToLower();
    }

    private class WrapArray<T>
    {
        private T[] array;

        public T[] Array => array;

        public WrapArray(T[] array)
        {
            this.array = array;
        }

        public T this[int index]
        {
            get => array[index % array.Length];
            set => array[index % array.Length] = value;
        }

        public int Length => array.Length;
    }
}