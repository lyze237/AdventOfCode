using System;
using AdventOfCodeLibrary.days;

namespace Day1
{
    public class Day1Section2 : ProgressDay
    {
        public Day1Section2() : base(1, 2)
        {
        }

        protected override object RunInternal(string input)
        {
            var changes = input.Split("\n");
            var frequency = 0;

            ProgressBar.MaxValue = changes.Length - 1;
            
            for (var i = 0; i < changes.Length; i++)
            {
                frequency += Convert.ToInt32(changes[i]);
                
                Update(i);
            }

            return frequency;
        }
    }
}
