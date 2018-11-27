using System.Threading;
using AdventOfCodeLibrary.days;

namespace TestDay
{
    public class TimeDaySection3 : TimeDay
    {
        public TimeDaySection3() : base(2, 0)
        {

        }

        protected override void RunInternal(string input)
        {
            Thread.Sleep(10000);
        }
    }
}
