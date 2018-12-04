using System;
using System.Linq;
using System.Collections.Generic;
using AdventOfCodeLibrary.days;
using Serilog;

namespace Day4
{
    public class Day4Section2 : ProgressDay
    {
        public Day4Section2() : base(4, 2)
        {
        }

        protected override object RunInternal(string input)
        {
            var lines = input.Split("\n").OrderBy(l => l).ToList();

            var times = new Dictionary<int, Dictionary<int, int>>();

            var guard = 0;
            var at = 0;

            ProgressBar.MaxValue = lines.Count;
            foreach (string line in lines) 
            {
                if (line.Contains("falls asleep")) 
                {
                    at = Convert.ToInt32(line.Split(":")[1].Split("]")[0]);
                }
                else if (line.Contains("wakes up")) 
                {
                    int wakeup = Convert.ToInt32(line.Split(":")[1].Split("]")[0]);
                    if (!times.ContainsKey(guard))
                        times.Add(guard, new Dictionary<int, int>());
                    
                    for (int min = at; min < wakeup; min++) {
                        if (!times[guard].ContainsKey(min))
                            times[guard].Add(min, 0);
                        
                        times[guard][min]++;
                    }
                }
                else 
                {
                    guard = Convert.ToInt32(line.Split("#")[1].Split(" ")[0]);
                }

                ProgressBar.Value++;
            }

            int sleepy = times
                .ToDictionary(t => t.Key, t => t.Value.OrderByDescending(tv => tv.Value).First())
                .OrderByDescending(t => t.Value.Value)
                .First().Key;

            int sleepyAt = times[sleepy].OrderByDescending(t => t.Value).First().Key;

            return sleepy * sleepyAt;
        }
    }
}
