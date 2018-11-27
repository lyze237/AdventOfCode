using System;

namespace AdventOfCodeLibrary.drawers
{
    public class PercentProgressBar : ProgressBar
    {
        #region Properties

        public ConsoleColor Text { get; set; }

        #endregion

        #region Constructors

        public PercentProgressBar(int x, int y, int width) : this (x, y, width, 0, 0, 100)
        {
            
        }

        public PercentProgressBar(int x, int y, int width, int value, int minValue, int maxValue) 
            : base(x, y, width, value, minValue, maxValue)
        {
            Text = ConsoleColor.White;
        }


        #endregion

        protected override void DrawInternal()
        {
            base.DrawInternal();

            var percent = (Value - MinValue) * 100 / (MaxValue - MinValue);
            var str = $"{percent} %";

            Console.ForegroundColor = Text;
            Console.SetCursorPosition(X + Width / 2 - str.Length / 2, Y);
            Console.Write(str);
        }
    }
}
