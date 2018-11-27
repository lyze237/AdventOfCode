using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCodeLibrary.drawers;

namespace AdventOfCodeLibrary.days
{
    public abstract class TimeDay : Day
    {
        protected TimeDay(int dayNumber, int section) : base(dayNumber, section)
        {
        }

        public override void SetupDrawer(int x, int y, int width)
        {
            Drawer = new TimeLeftBar(x, y, width);
        }
    }
}
