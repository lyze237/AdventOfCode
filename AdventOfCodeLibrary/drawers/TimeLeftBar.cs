//-----------------------------------------------------------------------
// <copyright>
// MIT License
//
// Copyright (c) 2018 Michael Weinberger lyze@owl.sh
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace AdventOfCodeLibrary.drawers
{
    public class TimeLeftBar : Drawer
    {
        private readonly ProgressBar progressBar;
        private readonly Spinner spinner;

        public long Time { get; set; }

        private DateTime? startedAt;

        public TimeLeftBar(int x, int y, int width, long time = -1) : base(x, y, width)
        {
            Time = time;

            progressBar = new ProgressBar(x, y, width) { MaxValue = time };
            spinner = new Spinner(x, y, width);
        }


        protected override void DrawInternal()
        {
            string text = DrawBar();

            Console.ResetColor();
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
        }

        public void Start()
        {
            startedAt = DateTime.Now;
        }
    }
}
