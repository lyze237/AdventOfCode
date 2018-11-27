using System;

namespace AdventOfCodeLibrary.drawers
{
    public class TimeLeftBar : Drawer
    {
        private readonly ProgressBar progressBar;
        private readonly Spinner spinner;

        public long Time { get; set; }

        public ConsoleColor Text { get; set; }

        private DateTime? startedAt;

        public TimeLeftBar(int x, int y, int width, long time = -1) : base(x, y, width)
        {
            Text = ConsoleColor.White;
            Time = time;

            progressBar = new ProgressBar(x, y, width) { MaxValue = time };
            spinner = new Spinner(x, y, width);
        }


        protected override void DrawInternal()
        {
            var text = DrawBar();

            Console.ForegroundColor = Text;
            Console.SetCursorPosition(X + Width / 2 - text.Length / 2, Y);
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

            if (Time == -1)
            {
                spinner.Draw();
                return timeSpan.ToString("G");
            }

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
            if (DoneTick)
            {
                Foreground = Errored ? ConsoleColor.Red : ConsoleColor.Green;

                Dirty = true;
            }

            UpdateDrawerColors(spinner);
            UpdateDrawerColors(progressBar);

            spinner.Tick();
            progressBar.Tick();

            Dirty = spinner.Dirty || progressBar.Dirty;
        }

        private void UpdateDrawerColors(Drawer drawer)
        {
            if (drawer.Foreground != Foreground)
                drawer.Foreground = Foreground;
            if (drawer.Background != Background)
                drawer.Background = Background;
        }

        public void Start()
        {
            startedAt = DateTime.Now;
        }
    }
}
