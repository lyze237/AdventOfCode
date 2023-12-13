using AoC.Framework;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day13 : Day<char[][][]>
{
    public Day13() : base(2023, 13, true)
    {
    }

    protected override object DoPart1(char[][][] input) =>
        Solve(input, false);


    protected override object DoPart2(char[][][] input) =>
        Solve(input, true);

    private static int Solve(IEnumerable<char[][]> input, bool hasSmudges)
    {
        var result = 0;

        foreach (var pattern in input)
        {
            // left to right is pattern.length, has 100 multiplier and can be accessed via pattern[i]
            // up to down 
            var directions = new (int length, int multiplier, Func<int, char[]> accessor)[]
            {
                (pattern.Length, 100, i => pattern[i]),
                (pattern[0].Length, 1, i => pattern.Select(row => row[i]).ToArray())
            };
            
            // iterating through both directions is the same with a different multiplier
            // so we can creat this tuple array to easily iterate through both
            result += directions.Sum(direction => CheckDirection(direction, hasSmudges));
        }

        return result;
    }

    private static int CheckDirection((int length, int multiplier, Func<int, char[]> accessor) direction, bool hasSmudges)
    {
        var resultInternal = 0;

        for (var i = 1; i < direction.length; i++)
        {
            var smudges = 0;
            for (var (goesRight, goesLeft) = (i, i - 1); goesRight < direction.length && goesLeft >= 0; (goesRight, goesLeft) = (goesRight + 1, goesLeft - 1))
                smudges += direction.accessor(goesRight).Zip(direction.accessor(goesLeft)).Count(it => it.First != it.Second);

            // first part doesn't have smudges
            // for second part we're ok if one result isn't correct, since then that can be treated as a smudge and still be accepted
            resultInternal += smudges == (hasSmudges ? 1 : 0) ? direction.multiplier * i : 0;
        }

        return resultInternal;
    }

    protected override char[][][] ParseInput(string input) =>
        input.Split("\n\n")
            .Select(desert => desert.Split("\n")
                .Select(line => line.ToCharArray())
                .ToArray())
            .ToArray();
}