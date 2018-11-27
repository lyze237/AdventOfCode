using System;

namespace AdventOfCode.drawers
{
    public class PercentProgressBar : ProgressBar
    {
        #region Properties

        public ConsoleColor Text { get; set; }

        #endregion

        #region Constructors

        public PercentProgressBar(int x, int y, int width, ConsoleColor text = ConsoleColor.White, ConsoleColor foreground = ConsoleColor.Cyan, ConsoleColor background = ConsoleColor.Black) : this (x, y, width, 0, 0, 100, text, foreground, background)
        {
            
        }

        public PercentProgressBar(int x, int y, int width, int value, int minValue, int maxValue, ConsoleColor text = ConsoleColor.White, ConsoleColor foreground = ConsoleColor.Cyan, ConsoleColor background = ConsoleColor.Black) 
            : base(x, y, width, value, minValue, maxValue, foreground, background)
        {
            Text = text;
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

        public override void Tick()
        {
        }
    }
}
