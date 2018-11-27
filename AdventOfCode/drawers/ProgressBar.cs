using System;

namespace AdventOfCode.drawers
{
    public class ProgressBar : Drawer
    {
        #region Fields

        private int width;
        private int minValue;
        private int value;
        private int maxValue;

        #endregion

        #region Properties

        public int MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                Dirty = true;
            }
        }

        public int Value
        {
            get => value;
            set
            {
                this.value = value;
                Dirty = true;
            }
        }

        public int MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                Dirty = true;
            }
        }

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

        public ProgressBar(int x, int y, int width, ConsoleColor foreground = ConsoleColor.Cyan, ConsoleColor background = ConsoleColor.Black) : this (x, y, width, 0, 0, 100, foreground, background)
        {
            
        }

        public ProgressBar(int x, int y, int width, int value, int minValue, int maxValue, ConsoleColor foreground = ConsoleColor.Cyan, ConsoleColor background = ConsoleColor.Black) : base(x, y, foreground, background)
        {
            Width = width;
            MinValue = minValue;
            Value = value;
            MaxValue = maxValue;
        }

        #endregion

        protected override void DrawInternal()
        {
            var newWidth = Width - 2;
            var newValue = (((Value - MinValue) * (newWidth - 0)) / (MaxValue - MinValue)) + 0;

            Console.Write('[');
            if (newValue - 1 > 0)
                Console.Write(new string('=', newValue - 1));
            Console.Write('>');
            if (Width - newValue > 0)
                Console.Write(new string(' ', newValue == 0 ? newWidth - 1 : newWidth - newValue));
            Console.Write(']');
        }

        public override void Tick()
        {
        }
    }
}
