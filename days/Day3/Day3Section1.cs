using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCodeLibrary.days;

namespace Day3
{
    public class Day3Section1 : ProgressDay
    {
        public Day3Section1() : base(3, 1)
        {
        }

        protected override object RunInternal(string input)
        {
            var lines = input.Split("\n");

            var field = new Dictionary<int, Dictionary<int, int>>();

            ProgressBar.MaxValue = lines.Length - 1;
            for (var i = 0; i < lines.Length; i++) 
            {
                var match = Regex.Match(lines[i], @"(?<claim>#(?<id>\d+) @ (?<l>\d+),(?<t>\d+): (?<w>\d+)x(?<h>\d+))");
                var groups = match.Groups;

                int id = Convert.ToInt32(groups["id"].Value);
                int left = Convert.ToInt32(groups["l"].Value);
                int top = Convert.ToInt32(groups["t"].Value);
                int width = Convert.ToInt32(groups["w"].Value);
                int height = Convert.ToInt32(groups["h"].Value);

                for (int x = left; x < left + width; x++) {
                    for (int y = top; y < top + height; y++) {
                        if (!field.ContainsKey(x))
                            field.Add(x, new Dictionary<int, int>());

                        if (!field[x].ContainsKey(y))
                            field[x].Add(y, 0);

                        field[x][y]++;
                    }
                }

                Update(i);
            }

            var amount = 0;
            foreach (int row in field.Keys) {
                foreach (int col in field[row].Keys) {
                    if (field[row][col] > 1)
                        amount++;
                }
            }

            return amount;
        }
    }
}
