using System.Collections.Generic;
using System.Text.RegularExpressions;
using Utils;

namespace Day21
{
    public class Day21Challenge1 : Challenge<int>
    {
        public Day21Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            Pattern inputPattern = new Pattern(new List<string> {
                ".#.",
                "..#",
                "###"
            });
            
            List<Rule> rules = new List<Rule>();
            var rulesRegex = new Regex(@"(.+) => (.+)");
            foreach (var line in GetInputFilePerLine())
            {
                var groups = rulesRegex.Match(line).Groups;
                rules.Add(new Rule(groups[1].Value, groups[2].Value));
            }

            for (int z = 0; z < 5; z++)
            {
                var newPattern = Slice(inputPattern, inputPattern.Length() % 2 == 0 ? 2 : 3);
                newPattern = Increase(newPattern, rules);

                inputPattern = Join(newPattern);
            }

            return inputPattern.Count(true);
        }

        public static Pattern Join(List<List<Pattern>> patterns)
        {
            Pattern newPattern = new Pattern();

            int newSize = -1;
            
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
        
        public static List<List<Pattern>> Increase(List<List<Pattern>> patterns, List<Rule> rules)
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
        
        public static List<List<Pattern>> Slice(Pattern input, int size)
        {
            List<List<Pattern>> newPattern = new List<List<Pattern>>();

            for (int y = 0; y < input.Length() / size; y++)
            {
                newPattern.Add(new List<Pattern>());
                for (int x = 0; x < input.Length() / size; x++)
                {
                    newPattern[y].Add(new Pattern(size));
                }
            }

            for (int y = 0; y < input.Length(); y++)
            {
                for (int x = 0; x < input.Length() / size; x++)
                {
                    newPattern[y / size][x].Content[y % size] = input.Content[y].Substring(x * size, size);
                }
            }

            return newPattern;
        }
    }
}