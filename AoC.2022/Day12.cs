using AoC.Framework;
using NUnit.Framework;

namespace AoC._2022;

[TestFixture]
public class Day12 : Day<char[][]>
{
    private static readonly List<(int x, int y)> Directions = new() { (0, 1), (0, -1), (-1, 0), (1, 0) };
    
    public Day12() : base(2022, 12, true) { }

    protected override object DoPart1(char[][] input) => 
        BreadthFirstSearch(ReplaceFindStartPoint(input)!.Value, input)!;

    protected override object? DoPart2(char[][] input)
    { 
        var lengths = new List<int?>();
        
        for (var y = 0; y < input.Length; y++)
            for (var x = 0; x < input[y].Length; x++)
                if (input[y][x] == 'a')
                    lengths.Add(BreadthFirstSearch((x, y), input));

        return lengths.Where(l => l != null).Min() ?? 0;
    }

    protected override char[][] ParseInput(string input) => 
        input.Split("\n").Select(s => s.ToCharArray()).ToArray();
    
    private (int x, int y)? ReplaceFindStartPoint(char[][] input)
    {
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] != 'S') 
                    continue;
                
                input[y][x] = 'a';
                return (x, y);
            }
        }

        return null;
    }

    private int? BreadthFirstSearch((int x, int y) start, char[][] input)
    {
        var queue = new Queue<(int x, int y, int steps)>();
        var visited = new HashSet<(int x, int y)>();

        queue.Enqueue((start.x, start.y, 0));

        while (queue.Count > 0)
        {
            var (x, y, steps) = queue.Dequeue();
            
            if (!visited.Add((x, y)))
                continue;

            if (input[y][x] == 'E')
                return steps;
            
            foreach (var (nX,  nY) in Directions)
            {
                var dX = x + nX;
                var dY = y +  nY;

                if (dY >= 0 && dY < input.Length && dX >= 0 && dX < input[0].Length)
                    if (input[dY][dX] - input[y][x] <= 1)
                        queue.Enqueue((dX, dY, steps + 1));
            }
        }

        return null;
    }
}