using System.Threading;
using AdventOfCodeLibrary.days;

namespace TestDay
{
    public class SpinnerDaySection2 : SpinnerDay
    {
        public SpinnerDaySection2() : base(1, 1)
        {

        }

        protected override void RunInternal(string input)
        {
            Thread.Sleep(1500);
        }
    }
}
