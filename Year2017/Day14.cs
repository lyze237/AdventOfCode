using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day14 : Day
{
    public override object ExecutePart1()
    {
        var set = 0;
        var knotField = new List<List<int>>();
        for (var i = 0; i < 128; i++)
        {
            var knot = CalculateKnot($"{Input}-{i}");

            var binaryKnot = string.Join(string.Empty,
                knot.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                )
            );
            Console.WriteLine(binaryKnot);
            set += binaryKnot.Count(c => c == '1');
        }

        return set;
    }

    public override object ExecutePart2()
    {
        var groups = 0;
        var things = new List<string>();
        for (var i = 0; i <= 127; i++)
        {
            things.Add($"{Input}-{i}");
        }

        var intRows = things.Select(thing =>
                KnotHash(thing).Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')))
            .Select(list => list.SelectMany(s => s.Select(c => Convert.ToInt32("" + c))).ToList()).ToList();

        for (var row = 0; row < intRows.Count; row++)
        {
            var intRow = intRows[row];
            for (var col = 0; col < intRow.Count; col++)
            {
                var i = intRow[col];
                if (i == 1)
                {
                    Eliminate(row, col, intRows);
                    groups++;
                }
            }
        }

        return groups;
    }

    private static string CalculateKnot(string input)
    {
        var lengths = input.ToCharArray().Select(c => (int)c)
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

    private string KnotHash(string input)
    {
        var lengths = input.ToCharArray().Select(c => (int)c)
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

    private static void Eliminate(int row, int col, List<List<int>> intRows)
    {
        intRows[row][col] = 0;
        try
        {
            if (intRows[row][col + 1] == 1)
                Eliminate(row, col + 1, intRows);
        }
        catch (Exception)
        {
        }

        try
        {
            if (intRows[row + 1][col] == 1)
                Eliminate(row + 1, col, intRows);
        }
        catch (Exception)
        {
        }

        try
        {
            if (intRows[row][col - 1] == 1)
                Eliminate(row, col - 1, intRows);
        }
        catch (Exception)
        {
        }

        try
        {
            if (intRows[row - 1][col] == 1)
                Eliminate(row - 1, col, intRows);
        }
        catch (Exception)
        {
        }
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