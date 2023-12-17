using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AoC.Framework;
using AoC.Framework.Data;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day17 : Day<int[][]>
{
    public record State(Point Point, Direction Direction, int MoveStreak);
    
    public Day17() : base(2023, 17, "102", "94")
    {
    }

    protected override object? DoPart1(int[][] input) => 
        Solve(input, 0, 3);

    protected override object? DoPart2(int[][] input) => 
        Solve(input, 4, 10);

    private static object? Solve(int[][] input, int minStreak, int maxStreak)
    {
        var start = new Point(0, 0);
        var end = new Point(input[0].Length - 1, input.Length - 1);
        
        var cost = new Dictionary<State, int>
        {
            { new State(start, Direction.Right, 0), 0 },
            { new State(start, Direction.Down, 0), 0 }
        };
        
        var queue = new PriorityQueue<State, int>();
        foreach (var (state, value) in cost)
            queue.Enqueue(state, value);

        while (queue.TryDequeue(out var state, out var heatLoss))
        {
            var (point, direction, moveStreak) = state;

            if (point == end && moveStreak >= minStreak)
                return heatLoss;

            var turns = new[] { DirectionInstruction.Forward, DirectionInstruction.TurnLeft, DirectionInstruction.TurnRight };
            foreach (var turn in turns)
            {
                var nextDirection = direction.Turn(turn);
                var nextPoint = point.Move(nextDirection);
                int? nextStreak = turn switch
                {
                    DirectionInstruction.Forward when moveStreak < maxStreak => moveStreak + 1,
                    DirectionInstruction.Forward => null,
                    _ when moveStreak >= minStreak => 1,
                    _ => null
                };

                if (nextStreak == null || !nextPoint.InRectangle(input))
                    continue;
                
                var nextCost = cost[state] + input[nextPoint.Y][nextPoint.X];
                var nextState = new State(nextPoint, nextDirection, nextStreak.Value);
                if (!cost.TryGetValue(nextState, out var value) || nextCost < value)
                {
                    cost[nextState] = nextCost;
                    queue.Enqueue(nextState, nextCost);
                }
            }
        }
        
        return null;
    }
    
    protected override int[][] ParseInput(string input) =>
        input.Split("\n")
            .Select(line => line.ToCharArray().Select(c => c.ToInt()).ToArray())
            .ToArray();
}