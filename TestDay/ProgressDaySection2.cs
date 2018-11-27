using System;
using System.Threading;
using AdventOfCodeLibrary;
using AdventOfCodeLibrary.days;
using AdventOfCodeLibrary.drawers;

namespace TestDay
{
    public class ProgressDaySection2 : ProgressDay
    {
        public ProgressDaySection2() : base(1, 0)
        {
        }

        protected override void RunInternal(string input)
        {
            var rnd = new Random();
            for (int i = 0; i <= 100; i++)
            {
                Update(i);
                Thread.Sleep(rnd.Next(0, 50));
            }
        }
    }
}
