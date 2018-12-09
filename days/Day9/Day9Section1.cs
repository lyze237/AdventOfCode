using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLibrary.days;

namespace Day9
{
    public class Day9Section1 : TimeDay
    {
        public Day9Section1() : base(9, 1)
        {
        }

        protected override object RunInternal(string input)
        {
            var strings = input.Split(" ");

            int players = Convert.ToInt32(strings[0]);
            int maxPoints = Convert.ToInt32(strings[6]);

            var scores = new Dictionary<int, int>();
            var circle = new LinkedList<int>();
            circle.AddFirst(0);

            for (var marble = 1; marble <= maxPoints; marble++)
            {
                if (marble % 23 == 0)
                {
                    circle.Rotate(-7);
                    int key = marble % players;
                    if (!scores.ContainsKey(key))
                        scores.Add(key, 0);
                    scores[key] += marble + circle.Last.Value;
                    circle.RemoveLast();
                }
                else
                {
                    circle.Rotate(2);
                    circle.AddLast(marble);
                }
            }

            return scores.Max(s => s.Value);
        }
    }
}
