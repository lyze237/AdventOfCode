using System;
using System.Linq;
using Utils;

namespace Day4
{
    public class Day4Challenge2 : Challenge<int>
    {
        public Day4Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            int okLines = 0;
            foreach (var line in GetInputFilePerLine())
            {
                var wordsArray = line.Split(" ");

                bool foundSame = false;

                for (var i = 0; i < wordsArray.Length; i++)
                {
                    string word = wordsArray[i];

                    if (wordsArray.Where((otherWord, j) => i != j).Any(otherWord => checkAnagram(word, otherWord)))
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

        private static bool checkAnagram(string our, string other)
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
}