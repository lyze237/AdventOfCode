using AdventOfCodeLibrary.drawers;

namespace AdventOfCodeLibrary.days
{
    public abstract class ProgressDay : Day
    {
        protected ProgressDay(int dayNumber, int section) : base(dayNumber, section)
        {

        }

        public override void SetupDrawer(int x, int y, int width)
        {
            Drawer = new PercentProgressBar(x, y, width);
        }

        public void Update(int value)
        {
            (Drawer as PercentProgressBar).Value = value;
        }

    }
}
