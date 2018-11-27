using System.Threading;
using AdventOfCodeLibrary;

namespace TestDay
{
    public class ProgressDaySection : Day
    {
        public ProgressDaySection() : base(0, 0, DayType.Progress)
        {

        }

        protected override void RunInternal(string input)
        {
            Thread.Sleep(5000);
        }
    }
}
