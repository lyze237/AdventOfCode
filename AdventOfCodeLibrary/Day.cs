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
                Console.SetCursorPosition(Drawer.X + Drawer.Width + 2, Drawer.Y);
                Console.ForegroundColor = ConsoleColor.Red;
                var msg = e.Message.Replace("\r", "").Replace("\n", " ");
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                Console.Write($"{frame.GetFileLineNumber()}/{frame.GetFileColumnNumber()} {msg}");
                Console.ResetColor();
                Drawer.Errored = true;
            }

            Drawer.Done = true;
        }

        protected abstract void RunInternal(string input);
    }
}
