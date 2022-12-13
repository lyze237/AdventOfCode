using AoC.Framework;
using NUnit.Framework;

namespace AoC._2022;

[TestFixture]
public class Day2 : Day
{
    public Day2() : base(2022, 2) { }

    protected override object DoPart1(string[] input) =>
        input.Select(line => line.Split(" "))
            .Select(i => (i[0], i[1]))
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

    protected override object DoPart2(string[] input) =>
        input.Select(line => line.Split(" "))
            .Select(i => (i[0], i[1]))
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