using System;
using System.Collections.Generic;
using AdventOfCodeLibrary.days;

namespace Day1
{
    public class Day1Section4 : TimeDay 
    {
        public Day1Section4() : base(1, 4)
        {
        }

        protected override object RunInternal(string input)
        {
            var recentFrequencies = new List<int> {0};
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
