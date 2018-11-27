using AdventOfCodeLibrary.drawers;

namespace AdventOfCodeLibrary.days
{
    public abstract class SpinnerDay : Day
    {
        protected SpinnerDay(int dayNumber, int section) : base(dayNumber, section)
        {
        }

        public override void SetupDrawer(int x, int y, int width)
        {
            Drawer = new Spinner(x, y, width);
        }
    }
}
