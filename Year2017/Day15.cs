using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day15 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var genAValue = Convert.ToInt64(Input[0].Split(' ').Last());
        var genBValue = Convert.ToInt64(Input[1].Split(' ').Last());

        var judge = 0;
        for (long i = 0; i < 40_000_000; i++)
        {
            genAValue = CalculateANext(genAValue);
            genBValue = CalculateBNext(genBValue);

            var genA = Convert.ToString(genAValue, 2);
            var genB = Convert.ToString(genBValue, 2);

            if (string.Join("", genA.Reverse().Take(16)) == string.Join("", genB.Reverse().Take(16)))
            {
                judge++;
            }
        }

        return judge;
    }

    public override object ExecutePart2()
    {
        var genAValue = Values(Convert.ToInt64(Input[0].Split(' ').Last()), 16807, 4);
        var genBValue = Values(Convert.ToInt64(Input[1].Split(' ').Last()), 48271, 8);

        var judge = 0;
        for (long i = 0; i < 5_000_000; i++)
        {
            genAValue.MoveNext();
            genBValue.MoveNext();

            if (genAValue.Current == genBValue.Current)
            {
                judge++;
            }
        }

        return judge;
    }

    private static IEnumerator<string> Values(long current, long factor, long modulo)
    {
        while (true)
        {
            current = current * factor % 2147483647;
            if (current % modulo == 0)
                yield return string.Join("", Convert.ToString(current, 2).Reverse().Take(16));
        }
    }

    private static long CalculateANext(long val)
    {
        val *= 16807;
        return val % 2147483647;
    }

    private static long CalculateBNext(long val)
    {
        val *= 48271;
        return val % 2147483647;
    }
}