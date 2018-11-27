using System;
using AdventOfCodeLibrary.drawers;
using owl.sh.owlutils.extensions;

namespace AdventOfCodeLibrary.days
{
    public abstract class TimeDay : Day
    {
        protected TimeDay(int dayNumber, int section) : base(dayNumber, section)
        {
        }

        public override void SetupDrawer(int x, int y, int width)
        {
            var time = -1L;
            if (timeFile.Exists)
                time = Convert.ToInt64(timeFile.ReadAllText());

            Drawer = new TimeLeftBar(x, y, width, time);
        }
    }
}
