using AdventOfCode.Parsers;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day9 : Day<string[]>.WithParser<SpaceParser>
{
    public override object ExecutePart1()
    {
        var players = Convert.ToInt32(Input[0]);
        var maxPoints = Convert.ToInt32(Input[6]);

        var scores = new Dictionary<int, int>();
        var circle = new LinkedList<int>();
        circle.AddFirst(0);

        for (var marble = 1; marble <= maxPoints; marble++)
        {
            if (marble % 23 == 0)
            {
                circle.Rotate(-7);
                var key = marble % players;
                if (!scores.ContainsKey(key))
                    scores.Add(key, 0);
                scores[key] += marble + circle.Last.Value;
                circle.RemoveLast();
            }
            else
            {
                circle.Rotate(2);
                circle.AddLast(marble);
            }
        }

        return scores.Max(s => s.Value);
    }

    public override object ExecutePart2()
    {
        long players = Convert.ToInt32(Input[0]);
        long maxPoints = Convert.ToInt32(Input[6]) * 100;

        var scores = new Dictionary<long, long>();
        var circle = new LinkedList<long>();
        circle.AddFirst(0);

        for (var marble = 1; marble <= maxPoints; marble++)
        {
            if (marble % 23 == 0)
            {
                circle.Rotate(-7);
                var key = marble % players;
                if (!scores.ContainsKey(key))
                    scores.Add(key, 0);
                scores[key] += marble + circle.Last.Value;
                circle.RemoveLast();
            }
            else
            {
                circle.Rotate(2);
                circle.AddLast(marble);
            }
        }

        return scores.Max(s => s.Value);
    }
}

internal static class LinkedListExtensions
{
    public static void Rotate<T>(this LinkedList<T> list, int amount)
    {
        if (list.Count <= 1)
            return;

        for (var i = 0; i < Math.Abs(amount); i++)
            list.Rotate(amount > 0);
    }

    private static void Rotate<T>(this LinkedList<T> list, bool right)
    {
        if (right)
        {
            var last = list.Last.Value;
            list.RemoveLast();
            list.AddFirst(last);
        }
        else
        {
            var first = list.First.Value;
            list.RemoveFirst();
            list.AddLast(first);
        }
    }
}