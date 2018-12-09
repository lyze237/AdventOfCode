using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLibrary.days;

namespace Day9
{
    public class Day9Section2 : TimeDay
    {
        public Day9Section2() : base(9, 2)
        {
        }

        protected override object RunInternal(string input)
        {
            var strings = input.Split(" ");

            long players = Convert.ToInt32(strings[0]);
            long maxPoints = Convert.ToInt32(strings[6]) * 100;

            var scores = new Dictionary<long, long>();
            var circle = new LinkedList<long>();
            circle.AddFirst(0);

            for (var marble = 1; marble <= maxPoints; marble++)
            {
                if (marble % 23 == 0)
                {
                    circle.Rotate(-7);
                    long key = marble % players;
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
