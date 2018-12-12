using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLibrary.days;
using owl.sh.owlutils.extensions;

namespace Day12
{
    public class Day12Section2 : ProgressDay
    {
        public Day12Section2() : base(12, 2)
        {
        }

        protected override object RunInternal(string input)
        {
            var lines = input.Split("\r\n").ToList();
            
            var pots = new DefaultDictionary<int, bool>();

            string states = lines[0].Replace("initial state: ", "");
            for (var i = 0; i < states.Length; i++)
                pots[i] = states[i] == '#';

            List<(List<bool>, bool)> mutations = lines.Skip(2)
                .Select(line => line.Split(" => "))
                .Select(thing => (
                    thing[0].Trim().Select(s => s == '#').ToList(), thing[1].Trim()[0] == '#')
                ).ToList();

            for (var i = 0; i <= 200; i++)
            {
                int min = pots.Where(p => p.Value).Min(pair => pair.Key) - 3;
                int max = pots.Where(p => p.Value).Max(pair => pair.Key) + 3;
                
                var newPots = new DefaultDictionary<int, bool>();
                for (int index = min; index < max; index++)
                {
                    var list = new List<bool>();
                    for (int j = index - 2; j <= index + 2; j++)
                        list.Add(pots[j]);

                    var mutationMatch = mutations.FirstOrDefault(m => m.Item1.SequenceEqual(list));
                    if (mutationMatch.IsDefault())
                        newPots[index] = false;
                    else
                        newPots[index] = mutationMatch.Item2;
                }
                pots = newPots;
            }

            int plants = pots.Count(p => p.Value);
            int sum = pots.Where(p => p.Value).Sum(p => p.Key);

            return (50000000000 - 201) * plants + sum;
        }
    }
}
