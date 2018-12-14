using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLibrary.days;

namespace Day14
{
    public class Day14Section2 : TimeDay
    {
        public Day14Section2() : base(14, 2)
        {
        }

        protected override object RunInternal(string input)
        {
            var lines = input.Split("\r\n");
            
            var recipes = lines[0].Select(c => c - '0').ToList();
            var recipeToFind = lines[1].Select(c => c - '0').ToList();
            
            var elves = new List<int> {0, 1};

            while (true)
            {
                int newRecipe = elves.Sum(e => recipes[e]);

                foreach (int numberToAdd in newRecipe.ToString().Select(c => c - '0'))
                {
                    recipes.Add(numberToAdd);
                    if (recipes.Count >= recipeToFind.Count)
                        if (recipes.Skip(recipes.Count - recipeToFind.Count).SequenceEqual(recipeToFind))
                            return recipes.Count - recipeToFind.Count;
                }
                

                for (var i = 0; i < elves.Count; i++)
                    elves[i] = (elves[i] + recipes[elves[i]] + 1) % recipes.Count;
            }
        }
    }
}
