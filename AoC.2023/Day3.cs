using AoC.Framework;
using AoC.Framework.Extensions;

namespace AoC._2023;

public class Day3 : Day<Day3.Part[]>
{
    public record Point(int Y, int X);
    public record Symbol(char Type, Point Point);
    public record Part(int Number, Symbol Symbol);
    
    private readonly (int y, int x)[] directions =
    {
        (-1, -1), (1, 0), (1, 1),
        (0, -1),          (0, 1),
        (1, -1), (-1, 0), (-1, 1)
    };
    
    public Day3() : base(2023, 3)
    {
    }

    protected override object DoPart1(Part[] input) => 
        input.Sum(part => part.Number);

    protected override object DoPart2(Part[] input) =>
        input
            .Where(part => part.Symbol.Type == '*') // works without this check too, oversight?
            .GroupBy(part => part.Symbol.Point)
            .Where(group => group.Count() == 2)
            .Sum(group => group.First().Number * group.Last().Number);

    protected override Part[] ParseInput(string inputString)
    {
        var input = inputString.Split("\n").Select(line => line.ToCharArray()).ToArray();
        
        var parts = new List<Part>();
        for (var y = 0; y < input.Length; y++)
        {
            var currentNumber = "";
            Symbol? symbol = null;
            for (var x = 0; x < input[y].Length; x++)
            {
                var c = input[y][x];

                if (char.IsDigit(c))
                {
                    currentNumber += c;
                    symbol ??= IsAdjacentToSymbol(input, y, x);
                }
                else if (!string.IsNullOrWhiteSpace(currentNumber))
                {
                    if (symbol != null)
                        parts.Add(new Part(currentNumber.ToInt(), symbol));

                    currentNumber = "";
                    symbol = null;
                }
            }

            if (!string.IsNullOrWhiteSpace(currentNumber) && symbol != null)
                parts.Add(new Part(currentNumber.ToInt(), symbol));
        }
        
        return parts.ToArray();
    }
    
    private Symbol? IsAdjacentToSymbol(char[][] input, int charY, int charX)
    {
        foreach (var (symbolY, symbolX) in directions)
        {
            var targetY = charY + symbolY;
            var targetX = charX + symbolX;

            if (targetY < 0 || targetX < 0 || targetY >= input.Length || targetX >= input[targetY].Length) 
                continue;
            
            if (!char.IsDigit(input[targetY][targetX]) && input[targetY][targetX] != '.')
                return new Symbol(input[targetY][targetX], new Point(targetY, targetX));
        }

        return null;
    }
}