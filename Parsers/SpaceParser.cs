using Tidy.AdventOfCode;

namespace AdventOfCode.Parsers;

public class SpaceParser : IParser<string[]>
{
    public string[] Parse(string rawInput) => 
        rawInput.Split(" ");
}