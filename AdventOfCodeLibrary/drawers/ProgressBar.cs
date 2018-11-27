using System;

namespace AdventOfCodeLibrary.drawers
{
    public class ProgressBar : Drawer
    {
        #region Fields

        private long minValue;
        private long value;
        private long maxValue;

        #endregion

        #region Properties

        public long MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                Dirty = true;
            }
        }

        public long Value
        {
            get => value;
            set
            {
                this.value = value;
                Dirty = true;
            }
        }

        public long MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                Dirty = true;
            }
        }

        #endregion

        #region Constructors

        public ProgressBar(int x, int y, int width) : this (x, y, width, 0, 0, 100)
        {
            
        }

        public ProgressBar(int x, int y, int width, long value, long minValue, long maxValue) : base(x, y, width)
        {
            MinValue = minValue;
            Value = value;
            MaxValue = maxValue;
        }


        #endregion

        protected override void DrawInternal()
        {
            var newWidth = Width - 2;
            var newValue = (Value - MinValue) * newWidth / (MaxValue - MinValue);

            Console.Write('[');
            if (newValue - 1 > 0)
                Console.Write(new string('=', (int) (newValue - 1)));
            Console.Write('>');
            if (Width - newValue > 0)
                Console.Write(new string(' ', (int) (newValue == 0 ? newWidth - 1 : newWidth - newValue)));
            Console.Write(']');
        }

        public override void Tick()
        {
            if (DoneTick)
            {
                Foreground = Value != MaxValue || Errored ? ConsoleColor.Red : ConsoleColor.Green;
                Dirty = true;
            }
        }
    }
}
