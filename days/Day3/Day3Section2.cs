using System;
using System.Collections.Generic;
using System.IO;
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
            string[] lines = input.Split("\n");

            Dictionary<int, Dictionary<int, int>> field = new Dictionary<int, Dictionary<int, int>>();

            ProgressBar.MaxValue = lines.Length - 1;

            var maxX = 0;
            var maxY = 0;

            for (int i = 0; i < lines.Length; i++) 
            {
                var match = Regex.Match(lines[i], @"(?<claim>#(?<id>\d+) @ (?<l>\d+),(?<t>\d+): (?<w>\d+)x(?<h>\d+))");
                var groups = match.Groups;

                var id = Convert.ToInt32(groups["id"].Value);
                var left = Convert.ToInt32(groups["l"].Value);
                var top = Convert.ToInt32(groups["t"].Value);
                var width = Convert.ToInt32(groups["w"].Value);
                var height = Convert.ToInt32(groups["h"].Value);

                for (int x = left; x < left + width; x++) {
                    for (int y = top; y < top + height; y++) {
                        if (!field.ContainsKey(x))
                            field.Add(x, new Dictionary<int, int>());

                        if (field[x].ContainsKey(y))
                        {
                            field[x][y] = -1;
                        }
                        else 
                        {
                            field[x].Add(y, id);
                        }

                        if (maxX < x)
                            maxX = x;

                        if (maxY < y)
                            maxY = y;
                    }
                }

                Update(i);
            }

            var validId = -1;
            foreach (var row in field.Keys) {
                foreach (var col in field[row].Keys) {
                    if (field[row][col] != -1) {
                        validId = field[row][col];
                    }
                }
            }

            var str = "";
            for (int x = 0; x < maxX; x++) {
                for (int y = 0; y < maxY; y++) {
                    if (field.ContainsKey(x) && field[x].ContainsKey(y))
                        str += " " + ("" + field[x][y]).PadRight(4) + " ";
                    else
                        str += " .... ";
                }
                str += "\n";
            }
            var f = new FileInfo("out.txt");
            File.WriteAllText(f.FullName, str);
            Console.Clear();

            Console.ResetColor();
            Console.WriteLine(f.FullName);

            Console.ReadKey();
            return validId;
        }
    }
}
