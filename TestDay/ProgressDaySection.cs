using System.Threading;
using AdventOfCodeLibrary;
using AdventOfCodeLibrary.days;
using AdventOfCodeLibrary.drawers;

namespace TestDay
{
    public class ProgressDaySection : ProgressDay
    {
        private PercentProgressBar bar;
        public ProgressDaySection() : base(0, 0)
        {
        }

        protected override void RunInternal(string input)
        {
            for (int i = 0; i <= 100; i++)
            {
                Update(i);
                Thread.Sleep(100);
            }
        }
    }
}
