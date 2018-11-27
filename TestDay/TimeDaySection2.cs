using System.Threading;
using AdventOfCodeLibrary.days;

namespace TestDay
{
    public class TimeDaySection2 : TimeDay
    {
        public TimeDaySection2() : base(1, 2)
        {

        }

        protected override void RunInternal(string input)
        {
            Thread.Sleep(2500);
        }
    }
}
