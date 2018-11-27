using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AdventOfCode.drawers
{
    public class TimeLeftBar : Drawer
    {
        private ProgressBar progressBar;
        private Spinner spinner;

        public int Width { get; set; }
        public long Time { get; set; }

        public ConsoleColor Text { get; set; }

        private DateTime? startedAt;

        public TimeLeftBar(int x, int y, int width, long time = -1, ConsoleColor text = ConsoleColor.White, ConsoleColor foreground = ConsoleColor.Cyan, ConsoleColor background = ConsoleColor.Black) : base(x, y, foreground, background)
        {
            Width = width;
            Text = text;
            Time = time;

            progressBar = new ProgressBar(x, y, width, foreground, background) { MaxValue = time };
            spinner = new Spinner(x, y, width, foreground, background);
        }


        protected override void DrawInternal()
        {
            var text = DrawBar();

            Console.ForegroundColor = Text;
            Console.SetCursorPosition((int) (X + Width / 2 - text.Length / 2), Y);
            Console.Write(text);
        }

        private string DrawBar()
        {
            if (!startedAt.HasValue)
            {
                spinner.Draw();
                return "Not started";
            }

            var now = DateTime.Now;
            var timeSpan = now - startedAt.Value;

            if (timeSpan.TotalMilliseconds > Time)
            {
                spinner.Draw();
                return $"Overtime... {timeSpan:G}";
            }

            progressBar.Value = (long) timeSpan.TotalMilliseconds;
            progressBar.Draw();
            return timeSpan.ToString("G");
        }

        public override void Tick()
        {
            spinner.Tick();
            progressBar.Tick();

            Dirty = spinner.Dirty || progressBar.Dirty;
        }

        public void Start()
        {
            startedAt = DateTime.Now;
        }
    }
}
