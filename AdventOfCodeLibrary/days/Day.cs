using System;
using System.Diagnostics;
using System.IO;
using AdventOfCodeLibrary.drawers;

namespace AdventOfCodeLibrary.days
{
    public abstract class Day
    {
        public int DayNumber { get; set; }
        public int Section { get; set; }

        private string input;
        public string Input => input ?? (input = File.Exists($"inputs/day{DayNumber}.{Section}.txt") ? File.ReadAllText($"inputs/day{DayNumber}.{Section}.txt") : null);

        public Drawer Drawer { get; set; }
        
        public Day(int dayNumber, int section)
        {
            DayNumber = dayNumber;
            Section = section;
        }
        public abstract void SetupDrawer(int x, int y, int width);

        public void Run(string input)
        {
            try
            {
                if (Drawer is TimeLeftBar bar)
                    bar.Start();

                RunInternal(input);
                LockConsole.WriteInAt($"Ok!", Drawer.X + Drawer.Width + 2, Drawer.Y, ConsoleColor.Green, ConsoleColor.Black);
            }
            catch (Exception e)
            {
                var msg = e.Message.Replace("\r", "").Replace("\n", " ");
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                LockConsole.WriteInAt($"{frame.GetFileLineNumber()}/{frame.GetFileColumnNumber()} {msg}", Drawer.X + Drawer.Width + 2, Drawer.Y, ConsoleColor.Red, ConsoleColor.Black);
                Drawer.Errored = true;
            }

            LockConsole.ResetColor();

            Drawer.Done = true;
        }

        protected abstract void RunInternal(string input);
    }
}
