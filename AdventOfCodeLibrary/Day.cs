using System;
using System.Diagnostics;
using System.IO;
using AdventOfCodeLibrary.drawers;

namespace AdventOfCodeLibrary
{
    public abstract class Day
    {
        public int DayNumber { get; set; }
        public int Section { get; set; }
        public DayType Type { get; set; }

        private string input;
        public string Input => input ?? (input = File.Exists($"inputs/day{DayNumber}.{Section}.txt") ? File.ReadAllText($"inputs/day{DayNumber}.{Section}.txt") : null);

        public Drawer Drawer { get; set; }
        
        public Day(int dayNumber, int section, DayType type)
        {
            DayNumber = dayNumber;
            Section = section;
            Type = type;
        }

        public void Run(string input)
        {
            try
            {
                if (Drawer is TimeLeftBar bar)
                    bar.Start();

                RunInternal(input);

            }
            catch (Exception e)
            {
                var msg = e.Message.Replace("\r", "").Replace("\n", " ");
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                LockConsole.WriteInAt($"{frame.GetFileLineNumber()}/{frame.GetFileColumnNumber()} {msg}", Drawer.X + Drawer.Width + 2, Drawer.Y, ConsoleColor.Red, ConsoleColor.Black);
                LockConsole.ResetColor();
                Drawer.Errored = true;
            }

            Drawer.Done = true;
        }

        protected abstract void RunInternal(string input);
    }
}
