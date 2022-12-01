using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day4 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        return Input.Select(line => line.Split(" "))
            .Count(wordsArray => new HashSet<string>(wordsArray).Count == wordsArray.Length);
    }

    public override object ExecutePart2()
    {
        var okLines = 0;
        
        foreach (var line in Input)
        {
            var wordsArray = line.Split(" ");

            var foundSame = false;

            for (var i = 0; i < wordsArray.Length; i++)
            {
                var word = wordsArray[i];

                if (wordsArray.Where((_, j) => i != j).Any(otherWord => CheckAnagram(word, otherWord)))
                {
                    foundSame = true;
                }

                if (foundSame)
                    break;
            }

            if (!foundSame)
                okLines++;
        }

        return okLines;
    }

    private static bool CheckAnagram(string our, string other)
    {
        if (our == other)
            return true;

        if (our.Length != other.Length)
            return false;

        var ourArray = our.ToCharArray();
        var otherArray = other.ToCharArray();

        Array.Sort(ourArray);
        Array.Sort(otherArray);

        var result = new string(ourArray) == new string(otherArray);
        if (result)
        {
            Console.WriteLine($"{our} is anagram of {other}");
        }

        return result;
    }
}