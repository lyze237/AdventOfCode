using Tidy.AdventOfCode;

namespace AdventOfCode.Parsers;

public class CommaParser : IParser<string[]>
{
    public string[] Parse(string rawInput) => 
        rawInput.Split(",");
}