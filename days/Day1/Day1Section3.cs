using System;
using System.Collections.Generic;
using AdventOfCodeLibrary.days;

namespace Day1
{
    public class Day1Section3 : TimeDay 
    {
        public Day1Section3() : base(1, 3)
        {
        }

        protected override object RunInternal(string input)
        {
            var recentFrequencies = new SortedSet<int> {0};
            var changes = input.Split("\n");
            var frequency = 0;

            while (true)
            {
                foreach (string change in changes)
                {
                    frequency += Convert.ToInt32(change);

                    if (recentFrequencies.Contains(frequency))
                        return frequency;

                    recentFrequencies.Add(frequency);
                }
            }
        }
    }
}
