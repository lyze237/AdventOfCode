using System;
using System.Threading;
using AdventOfCodeLibrary.days;

namespace TestDay
{
    public class SpinnerDaySection : SpinnerDay
    {
        public SpinnerDaySection() : base(0, 1)
        {

        }

        protected override void RunInternal(string input)
        {
            Thread.Sleep(1500);
            throw new ArgumentException("Null!", "param");
        }
    }
}
