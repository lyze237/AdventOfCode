using System.Linq;
using System.Collections.Generic;
using AdventOfCodeLibrary.days;

namespace Day2
{
    public class Day2Section1 : ProgressDay
    {
        public Day2Section1() : base(2, 1)
        {
        }

        protected override object RunInternal(string input)
        {
            var lines = input.Split("\n");

            ProgressBar.MaxValue = lines.Length - 1;

            var twos = 0;
            var threes = 0;
            
            for (var index = 0; index < lines.Length; index++)
            {
                string box = lines[index];

                var dictionary = new SortedDictionary<char, int>();
                
                foreach (char c in box)
                {
                    if (!dictionary.ContainsKey(c))
                        dictionary.Add(c, 0);

                    dictionary[c]++;
                }

                var threePairs = dictionary.Where(kvp => kvp.Value == 3).ToList();
                if (threePairs.Count > 0)
                    threes++;
                threePairs.ForEach(kvp => dictionary.Remove(kvp.Key));
                
                var twoPairs = dictionary.Where(kvp => kvp.Value == 2).ToList();
                if (twoPairs.Count > 0)
                    twos++;

                Update(index);
            }
            
            return twos * threes;
        }
    }
}
