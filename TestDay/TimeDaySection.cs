using System.Threading;
using AdventOfCodeLibrary.days;

namespace TestDay
{
    public class TimeDaySection : TimeDay
    {
        public TimeDaySection() : base(0, 2)
        {

        }

        protected override void RunInternal(string input)
        {
            Thread.Sleep(3500);
        }
    }
}
