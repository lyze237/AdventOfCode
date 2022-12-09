using AdventOfCode.Year2022.Extensins;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2022;

public class Day9 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1() =>
        CalculateRope(2);

    public override object ExecutePart2() =>
        CalculateRope(10);

    private int CalculateRope(int length)
    {
        var visited = new HashSet<(int, int)>();
        var rope = new (int x, int y)[length];

        foreach (var line in Input)
        {
            var (dir, cnt, _) = line.Split(" ");
            for (var move = 0; move < Convert.ToInt32(cnt); move++)
            {
                rope[0] = MoveTip(rope[0], dir!);

                for (var knot = 1; knot < length; knot++)
                    rope[knot] = MoveKnot(rope[knot - 1], rope[knot]);

                visited.Add(rope[^1]);
            }
        }

        return visited.Count;
    }

    private static (int x, int y) MoveTip((int x, int y) head, string dir) =>
        dir switch
        {
            "U" => (head.x, head.y - 1),
            "D" => (head.x, head.y + 1),
            "L" => (head.x - 1, head.y),
            "R" => (head.x + 1, head.y),
            _ => head
        };

    private static (int x, int y) MoveKnot((int x, int y) tip, (int x, int y) knot) =>
        (tip.x - knot.x, tip.y - knot.y) switch
        {
            (> 1, > 1) => (tip.x - 1, tip.y - 1),
            (< -1, > 1) => (tip.x + 1, tip.y - 1),
            (> 1, < -1) => (tip.x - 1, tip.y + 1),
            (< -1, < -1) => (tip.x + 1, tip.y + 1),
            (> 1, _) => (tip.x - 1, tip.y),
            (< -1, _) => (tip.x + 1, tip.y),
            (_, > 1) => (tip.x, tip.y - 1),
            (_, < -1) => (tip.x, tip.y + 1),
            _ => (knot.x, knot.y)
        };
}
