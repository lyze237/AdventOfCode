using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCodeLibrary.days;

namespace Day3
{
    public class Day3Section2 : ProgressDay
    {
        public Day3Section2() : base(3, 2)
        {
        }

        protected override object RunInternal(string input)
        {
            var lines = input.Split("\n");
            
            var table = new int[1000, 1000];
            var noOverlaps = new List<int>();
            
            ProgressBar.MaxValue = lines.Length - 1;

            for (var index = 0; index < lines.Length; index++)
            {
                var match = Regex.Match(lines[index], @"(?<claim>#(?<id>\d+) @ (?<l>\d+),(?<t>\d+): (?<w>\d+)x(?<h>\d+))");
                var matchGroups = match.Groups;

                int id = Convert.ToInt32(matchGroups["id"].Value);
                int left = Convert.ToInt32(matchGroups["l"].Value);
                int top = Convert.ToInt32(matchGroups["t"].Value);
                int width = Convert.ToInt32(matchGroups["w"].Value);
                int height = Convert.ToInt32(matchGroups["h"].Value);

                noOverlaps.Add(id);

                for (int x = left; x < left + width; x++)
                {
                    for (int y = top; y < top + height; y++)
                    {
                        int previousId = table[x, y];
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

                Update(index);
            }
            
            return noOverlaps.First();
        }
    }
}