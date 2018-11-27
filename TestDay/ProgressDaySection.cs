using System.Threading;
using AdventOfCodeLibrary;
using AdventOfCodeLibrary.drawers;

namespace TestDay
{
    public class ProgressDaySection : Day
    {
        private PercentProgressBar bar;
        public ProgressDaySection() : base(0, 0, DayType.Progress)
        {
        }

        protected override void RunInternal(string input)
        {
            bar = Drawer as PercentProgressBar;

            for (int i = 0; i <= 100; i++)
            {
                bar.Value = i;
                Thread.Sleep(100);
            }
        }
    }
}
