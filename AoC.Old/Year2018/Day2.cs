using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day2 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var twos = 0;
        var threes = 0;
            
        foreach (var box in Input)
        {
            var dictionary = new SortedDictionary<char, int>();
                
            foreach (var c in box)
            {
                if (!dictionary.ContainsKey(c))
                    dictionary.Add(c, 0);

                dictionary[c]++;
            }

            var threePairs = dictionary.Where(kvp => kvp.Value == 3).ToList();
            if (threePairs.Count > 0)
                threes++;
            threePairs.ForEach(kvp => dictionary.Remove(kvp.Key));
                
            var twoPairs = dictionary.Where(kvp => kvp.Value == 2).ToList();
            if (twoPairs.Count > 0)
                twos++;
        }
            
        return twos * threes;
    }

    public override object ExecutePart2()
    {
        foreach (var box in Input)
        {
            foreach (var compareBox in Input)
            {
                var sames = box
                    .Where((c, i) => c == compareBox[i])
                    .Aggregate("", (current, t) => current + t);

                if (sames.Length == box.Length - 1)
                    return sames;
            }
        }

        return null;
    }
}