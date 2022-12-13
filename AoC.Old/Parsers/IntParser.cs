using Tidy.AdventOfCode;

namespace AdventOfCode.Parsers;

public class IntParser : IParser<int>
{
    public int Parse(string rawInput) => 
        Convert.ToInt32(rawInput);
}