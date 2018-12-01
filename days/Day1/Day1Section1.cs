using System;
using System.Runtime.InteropServices;
using AdventOfCodeLibrary.days;

namespace Day1
{
    public class Day1Section1 : ProgressDay
    {
        public Day1Section1() : base(1, 1)
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
