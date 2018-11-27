using System;
using System.Threading;
using AdventOfCodeLibrary;

namespace TestDay
{
    public class SpinnerDaySection : Day
    {
        public SpinnerDaySection() : base(0, 1, DayType.Spinner)
        {

        }

        protected override void RunInternal(string input)
        {
            Thread.Sleep(1500);
            throw new ArgumentException("Null!", "param");
        }
    }
}
