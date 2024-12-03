using System.Text.RegularExpressions;
using AoC.Framework;
using AoC.Framework.Extensions;

namespace AoC._2024;

public partial class Day3() : Day<string>(2024, 3, true)
{
    [GeneratedRegex(@"mul\((?<left>\d+),(?<right>\d+)\)")]
    private static partial Regex Part1Regex();
    
    [GeneratedRegex(@"(?<mul>mul\((?<left>\d+),(?<right>\d+)\))|(?<do>do\(\))|(?<dont>don't\(\))")]
    private static partial Regex Part2Regex();
    
    protected override object DoPart1(string input) => 
        Part1Regex()
            .Matches(input)
            .Select(m => m.Groups["left"].Value.ToInt() * m.Groups["right"].Value.ToInt())
            .Sum();

    protected override object DoPart2(string input)
    {
        var enabled = true;
        var result = 0;
        
        foreach (Match match in Part2Regex().Matches(input))
        {
            if (match.Groups["do"].Success)
                enabled = true;
            else if (match.Groups["dont"].Success)
                enabled = false;
            
            if (enabled && match.Groups["mul"].Success)
                result += match.Groups["left"].Value.ToInt() * match.Groups["right"].Value.ToInt();
        }

        return result;
    }

    protected override string ParseInput(string input) => 
        input;
}