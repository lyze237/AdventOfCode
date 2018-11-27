using System.Threading;
using AdventOfCodeLibrary.days;

namespace TestDay
{
    public class ProgressDaySection : ProgressDay
    {
        public ProgressDaySection() : base(0, 0)
        {
        }

        protected override void RunInternal(string input)
        {
            for (int i = 0; i <= 100; i++)
            {
                Update(i);
                Thread.Sleep(25);
            }
        }
    }
}
