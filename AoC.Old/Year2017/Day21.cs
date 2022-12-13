using System.Text;
using System.Text.RegularExpressions;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day21 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var inputPattern = new Pattern(new List<string>
        {
            ".#.",
            "..#",
            "###"
        });

        var rulesRegex = new Regex(@"(.+) => (.+)");
        var rules = Input.Select(line => rulesRegex.Match(line).Groups)
            .Select(groups => new Rule(groups[1].Value, groups[2].Value)).ToList();

        for (var z = 0; z < 5; z++)
        {
            var newPattern = Slice(inputPattern, inputPattern.Length() % 2 == 0 ? 2 : 3);
            newPattern = Increase(newPattern, rules);

            inputPattern = Join(newPattern);
        }

        return inputPattern.Count(true);
    }

    public override object ExecutePart2()
    {
        var inputPattern = new Pattern(new List<string> {
            ".#.",
            "..#",
            "###"
        });

        var rulesRegex = new Regex(@"(.+) => (.+)");
        var rules = Input.Select(line => rulesRegex.Match(line).Groups).Select(groups => new Rule(groups[1].Value, groups[2].Value)).ToList();

        for (var z = 0; z < 18; z++)
        {
            var newPattern = Slice(inputPattern, inputPattern.Length() % 2 == 0 ? 2 : 3);
            newPattern = Increase(newPattern, rules);

            inputPattern = Join(newPattern);
        }

        return inputPattern.Count(true);
    }

    private static Pattern Join(List<List<Pattern>> patterns)
    {
        var newPattern = new Pattern();

        var newSize = -1;

        for (var y = 0; y < patterns.Count; y++)
        {
            for (var x = 0; x < patterns[y].Count; x++)
            {
                for (var i = 0; i < patterns[y][x].Content.Count; i++)
                {
                    if (newSize == -1 && y == 0)
                    {
                        if (newPattern.Content.Count <= i)
                            newPattern.Content.Add("");

                        newPattern.Content[i] += patterns[y][x].Content[i];
                    }
                    else if (newSize == -1)
                    {
                        newSize = newPattern.Content.Count;
                        if (newPattern.Content.Count <= i + y * newSize)
                            newPattern.Content.Add("");

                        newPattern.Content[i + y * newSize] += patterns[y][x].Content[i];
                    }
                    else
                    {
                        if (newPattern.Content.Count <= i + y * newSize)
                            newPattern.Content.Add("");

                        newPattern.Content[i + y * newSize] += patterns[y][x].Content[i];
                    }
                }
            }
        }

        return newPattern;
    }

    private static List<List<Pattern>> Increase(List<List<Pattern>> patterns, List<Rule> rules)
    {
        foreach (var patternSub in patterns)
        {
            for (var i = 0; i < patternSub.Count; i++)
            {
                var pattern = patternSub[i];
                foreach (var rule in rules)
                {
                    if (rule.Matches(pattern))
                    {
                        patternSub[i] = rule.To;
                        break;
                    }
                }
            }
        }

        return patterns;
    }

    private static List<List<Pattern>> Slice(Pattern input, int size)
    {
        var newPattern = new List<List<Pattern>>();

        for (var y = 0; y < input.Length() / size; y++)
        {
            newPattern.Add(new List<Pattern>());
            for (var x = 0; x < input.Length() / size; x++)
            {
                newPattern[y].Add(new Pattern(size));
            }
        }

        for (var y = 0; y < input.Length(); y++)
        {
            for (var x = 0; x < input.Length() / size; x++)
            {
                newPattern[y / size][x].Content[y % size] = input.Content[y].Substring(x * size, size);
            }
        }

        return newPattern;
    }

    private class Pattern
    {
        public List<string> Content { get; set; }

        public Pattern(List<string> content)
        {
            Content = content;
        }

        public Pattern(int size) : this()
        {
            for (var i = 0; i < size; i++)
            {
                Content.Add(new string(' ', size));
            }
        }

        public Pattern()
        {
            Content = new List<string>();
        }

        public int Length()
        {
            return Content[0].Length;
        }

        public static bool operator ==(Pattern first, Pattern second)
        {
            if (first.Length() != second.Length())
                return false;

            for (var i = 0; i < first.Content.Count; i++)
            {
                if (first.Content[i] != second.Content[i])
                    return false;
            }

            return true;
        }

        public static bool operator !=(Pattern first, Pattern second)
        {
            return !(first == second);
        }

        public int Count(bool on)
        {
            return Content.Sum(line => line.Count(c => on && c == '#' || !on && c == '.'));
        }
    }

    private class Rule
    {
        public List<Pattern> Froms { get; set; }
        public Pattern To { get; set; }

        public Rule(Pattern from, Pattern to)
        {
            To = to;

            GenerateFroms(from);
        }

        public Rule(string from, string to)
        {
            var fromPattern = new Pattern();
            Froms = new List<Pattern>();
            To = new Pattern();

            foreach (var line in from.Split('/'))
            {
                fromPattern.Content.Add(line);
            }

            foreach (var line in to.Split('/'))
            {
                To.Content.Add(line);
            }

            GenerateFroms(fromPattern);
        }

        private void GenerateFroms(Pattern from)
        {
            Froms.Add(Rotate(from));
            for (var z = 0; z < 3; z++)
            {
                Froms.Add(Rotate(Froms.Last()));
            }

            from = FlipY(from);
            Froms.Add(Rotate(from));
            for (var z = 0; z < 3; z++)
            {
                Froms.Add(Rotate(Froms.Last()));
            }

            from = FlipX(from);
            Froms.Add(Rotate(from));
            for (var z = 0; z < 3; z++)
            {
                Froms.Add(Rotate(Froms.Last()));
            }

            from = FlipY(from);
            Froms.Add(Rotate(from));
            for (var z = 0; z < 3; z++)
            {
                Froms.Add(Rotate(Froms.Last()));
            }

            from = FlipX(from);
            Froms.Add(Rotate(from));
            for (var z = 0; z < 3; z++)
            {
                Froms.Add(Rotate(Froms.Last()));
            }
        }

        private Pattern FlipY(Pattern pattern)
        {
            var newPattern = new Pattern(pattern.Length());

            for (var i = 0; i < newPattern.Length(); i++)
            {
                newPattern.Content[i] = pattern.Content[i];
            }

            newPattern.Content.Reverse();

            return newPattern;
        }

        private Pattern FlipX(Pattern pattern)
        {
            var newPattern = new Pattern(pattern.Length());

            for (var i = 0; i < newPattern.Length(); i++)
            {
                newPattern.Content[i] = pattern.Content[i];
                newPattern.Content[i].Reverse();
            }

            return newPattern;
        }

        private Pattern Rotate(Pattern from)
        {
            var pattern = new Pattern(from.Length());


            for (var i = from.Content.Count - 1; i >= 0; i--)
            {
                for (var j = 0; j < from.Content[i].Length; j++)
                {
                    var sb = new StringBuilder(pattern.Content[j]);
                    sb[from.Content.Count - 1 - i] = from.Content[i][j];
                    pattern.Content[j] = sb.ToString();
                }
            }

            return pattern;
        }

        public bool Matches(Pattern input)
        {
            return Froms.Any(pattern => input == pattern);
        }
    }
}