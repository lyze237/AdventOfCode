using Tidy.AdventOfCode;

namespace AdventOfCode.Year2022;

public class Day12 : Day<char[][]>
{
    private static readonly List<(int x, int y)> Directions = new() { (0, 1), (0, -1), (-1, 0), (1, 0) };
    
    public override char[][] ParseInput(string rawInput) => 
        rawInput.Split("\n").Select(s => s.ToCharArray()).ToArray();

    public override object ExecutePart1() => 
        BreadthFirstSearch(ReplaceFindStartPoint()!.Value)!;

    public override object ExecutePart2()
    {
        var lengths = new List<int?>();
        
        for (var y = 0; y < Input.Length; y++)
            for (var x = 0; x < Input[y].Length; x++)
                if (Input[y][x] == 'a')
                    lengths.Add(BreadthFirstSearch((x, y)));

        return lengths.Where(l => l != null).Min() ?? 0;
    }

    private (int x, int y)? ReplaceFindStartPoint()
    {
        for (var y = 0; y < Input.Length; y++)
        {
            for (var x = 0; x < Input[y].Length; x++)
            {
                if (Input[y][x] != 'S') 
                    continue;
                
                Input[y][x] = 'a';
                return (x, y);
            }
        }

        return null;
    }

    private int? BreadthFirstSearch((int x, int y) start)
    {
        var queue = new Queue<(int x, int y, int steps)>();
        var visited = new HashSet<(int x, int y)>();

        queue.Enqueue((start.x, start.y, 0));

        while (queue.Count > 0)
        {
            var (x, y, steps) = queue.Dequeue();
            
            if (!visited.Add((x, y)))
                continue;

            if (Input[y][x] == 'E')
                return steps;
            
            foreach (var (nX,  nY) in Directions)
            {
                var dX = x + nX;
                var dY = y +  nY;

                if (dY >= 0 && dY < Input.Length && dX >= 0 && dX < Input[0].Length)
                    if (Input[dY][dX] - Input[y][x] <= 1)
                        queue.Enqueue((dX, dY, steps + 1));
            }
        }

        return null;
    }
}