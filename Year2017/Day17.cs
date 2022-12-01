using AdventOfCode.Parsers;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day17 : Day<int>.WithParser<IntParser>
{
    public override object ExecutePart1()
    {
        var buffer = new List<int> { 0 };

        var currentPosition = 0;

        for (var i = 1; i <= 2017; i++)
        {
            currentPosition = (currentPosition + Input % buffer.Count) % buffer.Count;
            buffer.Insert(++currentPosition, i);
        }

        return buffer[buffer.IndexOf(2017) + 1];
    }

    public override object ExecutePart2()
    {
        var currentPosition = 0;
        var result = 0;

        for (var i = 0; i < 50_000_000; i++)
        {
            currentPosition = (currentPosition + Input % (i + 1)) % (i + 1);
            if (currentPosition++ == 0)
            {
                result = i + 1;
            }
        }

        return result;
    }
}