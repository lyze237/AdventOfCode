using System.Threading;
using AdventOfCodeLibrary;

namespace TestDay
{
    public class TimeDaySection : Day
    {
        public TimeDaySection() : base(0, 2, DayType.Time)
        {

        }

        protected override void RunInternal(string input)
        {
            Thread.Sleep(3500);
        }
    }
}
