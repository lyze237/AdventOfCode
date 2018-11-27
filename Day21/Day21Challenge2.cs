//-----------------------------------------------------------------------
// <copyright>
// MIT License
//
// Copyright (c) 2018 Michael Weinberger lyze@owl.sh
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.RegularExpressions;
using Utils;

namespace Day21
{
    public class Day21Challenge2 : Challenge<int>
    {
        public Day21Challenge2() : base("input")
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

            for (int z = 0; z < 18; z++)
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
