using AoC.Framework;
using AoC.Framework.Extensions;

namespace AoC._2023;

public class Day14 : Day<char[][]>
{
    public Day14() : base(2023, 14)
    {
    }

    protected override object DoPart1(char[][] input) => 
        CountScore(RunStep(input));

    protected override object? DoPart2(char[][] input)
    {
        var seen = new List<string>();
        
        for (var counter = 1_000_000_000; counter >= 0; counter--)
        {
            input = RunCycle(input);
            var hash = string.Join(' ', input.SelectMany(line => line));
            
            if (seen.Contains(hash))
            {
                for (var restCyclesToRun = (counter - 1) % (seen.Count - seen.IndexOf(hash)); restCyclesToRun > 0; restCyclesToRun--)
                    input = RunCycle(input);
                
                return CountScore(input);
            }
            
            seen.Add(hash);
        }

        return null;
    }
    
    private static char[][] RunCycle(char[][] input)
    {
        for (var i = 0; i < 4; i++)
            input = RunStep(input).Rotate();

        return input;
    }

    private static char[][] RunStep(char[][] input)
    {
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[0].Length; x++)
            {
                if (input[y][x] != 'O')
                    continue;

                var newY = FindNewY(input, y, x);
                if (newY.HasValue)
                {
                    input[newY.Value][x] = 'O';
                    input[y][x] = '.';
                }
            }
        }

        return input;
    }

    private static long? FindNewY(IReadOnlyList<char[]> input, int y, int x)
    {
        long? newY = null;
        for (var yMove = y - 1; yMove >= 0; yMove--)
        {
            if (input[yMove][x] != '.')
                break;

            newY = yMove;
        }

        return newY;
    }

    private static int CountScore(IReadOnlyList<char[]> input)
    {
        var totalScore = 0;
        for (var (y, score) = (0, input.Count); y < input.Count; (y, score) = (y + 1, score - 1))
        {
            for (var x = 0; x < input[0].Length; x++)
            {
                if (input[y][x] == 'O')
                    totalScore += score;
            }
        }

        return totalScore;
    }

    protected override char[][] ParseInput(string input) =>
        input.Split("\n").Select(line => line.ToCharArray()).ToArray();
}