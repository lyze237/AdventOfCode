using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLibrary.days;

namespace Day14
{
    public class Day14Section1 : TimeDay
    {
        public Day14Section1() : base(14, 1)
        {
        }

        protected override object RunInternal(string input)
        {
            var lines = input.Split("\r\n");
            
            var recipes = lines[0].Select(c => c - '0').ToList();

            var elves = new List<int> {0, 1};

            var n = Convert.ToInt32(lines[1]);

            while (recipes.Count < n + 10)
            {
                int newRecipe = elves.Sum(e => recipes[e]);

                recipes.AddRange(("" + newRecipe).Select(c => c - '0').ToList());

                for (var i = 0; i < elves.Count; i++)
                {
                    elves[i] = (elves[i] + recipes[elves[i]] + 1) % recipes.Count;
                }
            }

            return string.Join("", recipes.Skip(n).Take(10));
        }
    }
}
