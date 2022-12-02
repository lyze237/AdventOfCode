using Tidy.AdventOfCode;

namespace AdventOfCode.Year2022;

public class Day2 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        return Input.Select(line => line.Split(" "))
            .Select(input => (input[0], input[1]))
            .Select(match => match switch
            {
                ("A", "X") => 1 + 3,
                ("A", "Y") => 2 + 6,
                ("A", "Z") => 3 + 0,

                ("B", "X") => 1 + 0,
                ("B", "Y") => 2 + 3,
                ("B", "Z") => 3 + 6,

                ("C", "X") => 1 + 6,
                ("C", "Y") => 2 + 0,
                ("C", "Z") => 3 + 3,

                _ => throw new ArgumentOutOfRangeException()
            })
            .Sum();
    }

    public override object ExecutePart2()
    {
        return Input.Select(line => line.Split(" "))
            .Select(input => (input[0], input[1]))
            .Select(match => match switch
            {
                ("A", "X") => 3 + 0,
                ("A", "Y") => 1 + 3,
                ("A", "Z") => 2 + 6,

                ("B", "X") => 1 + 0,
                ("B", "Y") => 2 + 3,
                ("B", "Z") => 3 + 6,

                ("C", "X") => 2 + 0,
                ("C", "Y") => 3 + 3,
                ("C", "Z") => 1 + 6,

                _ => throw new ArgumentOutOfRangeException()
            })
            .Sum();
    }
}