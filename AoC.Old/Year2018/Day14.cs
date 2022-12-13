using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day14 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var recipes = Input[0].Select(c => c - '0').ToList();

        var elves = new List<int> {0, 1};

        var n = Convert.ToInt32(Input[1]);

        while (recipes.Count < n + 10)
        {
            var newRecipe = elves.Sum(e => recipes[e]);

            recipes.AddRange(("" + newRecipe).Select(c => c - '0').ToList());

            for (var i = 0; i < elves.Count; i++)
            {
                elves[i] = (elves[i] + recipes[elves[i]] + 1) % recipes.Count;
            }
        }

        return string.Join("", recipes.Skip(n).Take(10));
    }

    public override object ExecutePart2()
    {
        var recipes = Input[0].Select(c => c - '0').ToList();
        var recipeToFind = Input[1].Select(c => c - '0').ToList();
            
        var elves = new List<int> {0, 1};

        while (true)
        {
            var newRecipe = elves.Sum(e => recipes[e]);

            foreach (var numberToAdd in newRecipe.ToString().Select(c => c - '0'))
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