using System.Text.RegularExpressions;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day3 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var field = new Dictionary<int, Dictionary<int, int>>();

        foreach (var line in Input)
        {
            var match = Regex.Match(line, @"(?<claim>#(?<id>\d+) @ (?<l>\d+),(?<t>\d+): (?<w>\d+)x(?<h>\d+))");
            var groups = match.Groups;

            var id = Convert.ToInt32(groups["id"].Value);
            var left = Convert.ToInt32(groups["l"].Value);
            var top = Convert.ToInt32(groups["t"].Value);
            var width = Convert.ToInt32(groups["w"].Value);
            var height = Convert.ToInt32(groups["h"].Value);

            for (var x = left; x < left + width; x++) {
                for (var y = top; y < top + height; y++) {
                    if (!field.ContainsKey(x))
                        field.Add(x, new Dictionary<int, int>());

                    if (!field[x].ContainsKey(y))
                        field[x].Add(y, 0);

                    field[x][y]++;
                }
            }
        }

        return field.Keys.Sum(row => field[row].Keys.Count(col => field[row][col] > 1));
    }

    public override object ExecutePart2()
    {
        var table = new int[1000, 1000];
        var noOverlaps = new List<int>();

        foreach (var line in Input)
        {
            var match = Regex.Match(line, @"(?<claim>#(?<id>\d+) @ (?<l>\d+),(?<t>\d+): (?<w>\d+)x(?<h>\d+))");
            var matchGroups = match.Groups;

            var id = Convert.ToInt32(matchGroups["id"].Value);
            var left = Convert.ToInt32(matchGroups["l"].Value);
            var top = Convert.ToInt32(matchGroups["t"].Value);
            var width = Convert.ToInt32(matchGroups["w"].Value);
            var height = Convert.ToInt32(matchGroups["h"].Value);

            noOverlaps.Add(id);

            for (var x = left; x < left + width; x++)
            {
                for (var y = top; y < top + height; y++)
                {
                    var previousId = table[x, y];
                    if (previousId == 0)
                    {
                        table[x, y] = id;
                    }
                    else
                    {
                        noOverlaps.Remove(id);
                        noOverlaps.Remove(previousId);
                    }
                }
            }
        }
            
        return noOverlaps.First();
    }
}