using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day12 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var pots = new Dictionary<int, bool>();

        var states = Input[0].Replace("initial state: ", "");
        for (var i = 0; i < states.Length; i++)
            pots[i] = states[i] == '#';

        var mutations = Input.Skip(2)
            .Select(line => line.Split(" => "))
            .Select(thing => (
                thing[0].Trim().Select(s => s == '#').ToList(), thing[1].Trim()[0] == '#')
            ).ToList();

        for (var i = 0; i < 20; i++)
        {
            var min = pots.Where(p => p.Value).Min(pair => pair.Key) - 3;
            var max = pots.Where(p => p.Value).Max(pair => pair.Key) + 3;
                
            var newPots = new Dictionary<int, bool>();
            for (var index = min; index < max; index++)
            {
                var list = new List<bool>();
                for (var j = index - 2; j <= index + 2; j++)
                    list.Add(pots[j]);

                var mutationMatch = mutations.FirstOrDefault(m => m.Item1.SequenceEqual(list));
                if (mutationMatch.Equals(default))
                    newPots[index] = false;
                else
                    newPots[index] = mutationMatch.Item2;
            }
            pots = newPots;
        }

        return pots.Where(p => p.Value).Sum(p => p.Key);
    }

    public override object ExecutePart2()
    {
        var pots = new Dictionary<int, bool>();

        var states = Input[0].Replace("initial state: ", "");
        for (var i = 0; i < states.Length; i++)
            pots[i] = states[i] == '#';

        var mutations = Input.Skip(2)
            .Select(line => line.Split(" => "))
            .Select(thing => (
                thing[0].Trim().Select(s => s == '#').ToList(), thing[1].Trim()[0] == '#')
            ).ToList();

        for (var i = 0; i <= 200; i++)
        {
            var min = pots.Where(p => p.Value).Min(pair => pair.Key) - 3;
            var max = pots.Where(p => p.Value).Max(pair => pair.Key) + 3;
                
            var newPots = new Dictionary<int, bool>();
            for (var index = min; index < max; index++)
            {
                var list = new List<bool>();
                for (var j = index - 2; j <= index + 2; j++)
                    list.Add(pots[j]);

                var mutationMatch = mutations.FirstOrDefault(m => m.Item1.SequenceEqual(list));
                if (mutationMatch.Equals(default))
                    newPots[index] = false;
                else
                    newPots[index] = mutationMatch.Item2;
            }
            pots = newPots;
        }

        var plants = pots.Count(p => p.Value);
        var sum = pots.Where(p => p.Value).Sum(p => p.Key);

        return (50000000000 - 201) * plants + sum;
    }
}