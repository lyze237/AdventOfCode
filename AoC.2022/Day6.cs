using AoC.Framework;
using NUnit.Framework;

namespace AoC._2022;

[TestFixture]
public class Day6 : Day<string>
{
    public Day6() : base(2022, 6, true) { }

    protected override object DoPart1(string input) =>
        FindPacket(4, input);

    protected override object DoPart2(string input) => 
        FindPacket(14, input);

    protected override string ParseInput(string input) => 
        input;
    
    private static int FindPacket(int length, string input)
    {
        for (var i = length; i < input.Length; i++)
            if (input.Substring(i - length, length).Distinct().Count() == length)
                return i;

        return -1;
    }
}