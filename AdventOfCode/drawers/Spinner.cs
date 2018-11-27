using System;

namespace AdventOfCode.drawers
{
    public class Spinner : Drawer
    {
        #region Fields

        private int value;
        private int width;

        private bool right;

        private int tickCount = 0;
        public int UpdatesAfterTicks = 1;


        #endregion


        #region Properties

        public int Width
        {
            get => width;
            set
            {
                width = value;
                Dirty = true;
            }
        }

        #endregion

        #region Constructors

        public Spinner(int x, int y, int width, ConsoleColor foreground = ConsoleColor.Cyan, ConsoleColor background = ConsoleColor.Black) : base(x, y, foreground, background)
        {
            Width = width;
        }

        #endregion

        protected override void DrawInternal()
        {
            var newWidth = Width - 2;
            var newValue = (((value - 0) * (newWidth - 0)) / (Width - 0)) + 0;

            Console.Write('[');
            if (newValue - 1 > 0)
                Console.Write(new string(' ', newValue - 1));
            Console.Write('=');
            if (Width - newValue > 0)
                Console.Write(new string(' ', newValue == 0 ? newWidth - 1 : newWidth - newValue));
            Console.Write(']');
        }

        public override void Tick()
        {
            if (++tickCount < UpdatesAfterTicks)
                return;

            tickCount = 0;

            if (right)
            {
                if (++value > Width)
                {
                    right = false;
                    value = Width;
                }
            }
            else
            {
                if (--value < 0)
                {
                    right = true;
                    value = 0;
                }
            }

            Dirty = true;
        }
    }
}
