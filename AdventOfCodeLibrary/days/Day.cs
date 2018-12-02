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
using System.Diagnostics;
using System.IO;
using AdventOfCodeLibrary.drawers;
using owl.sh.owlutils.extensions;

namespace AdventOfCodeLibrary.days
{
    public abstract class Day
    {
        public int DayNumber { get; set; }
        public int Section { get; set; }

        private string input;
        public string Input => input ?? (input = File.Exists($"inputs/day{DayNumber}.{Section}.txt") ? File.ReadAllText($"inputs/day{DayNumber}.{Section}.txt") : null);

        public Drawer Drawer { get; set; }

        protected FileInfo timeFile;
        
        public Day(int dayNumber, int section)
        {
            DayNumber = dayNumber;
            Section = section;

            var dir = new DirectoryInfo("times");
            if (!dir.Exists)
                dir.Create();
            timeFile = dir.GetFileInDirectory($"{dayNumber}.{section}.txt");
        }
        public abstract void SetupDrawer(int x, int y, int width);

        public void Run(string input)
        {
            var now = DateTime.Now;
            try
            {
                if (Drawer is TimeLeftBar bar)
                    bar.Start();

                var result = RunInternal(input);
                LockConsole.WriteInAt($"Success: {result ?? "null"}", Drawer.X + Drawer.Width + 2, Drawer.Y, ConsoleColor.Green);
            }
            catch (Exception e)
            {
                string msg = e.Message.Replace("\r", "").Replace("\n", " ");
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                LockConsole.WriteInAt($"{frame.GetFileLineNumber()}/{frame.GetFileColumnNumber()} {msg}", Drawer.X + Drawer.Width + 2, Drawer.Y, ConsoleColor.Red);
                Drawer.Errored = true;
            }
            var timeSpan = DateTime.Now - now;
                  
            File.WriteAllText(timeFile.FullName, "" + (long) timeSpan.TotalMilliseconds);

            LockConsole.ResetColor();

            Drawer.Done = true;
        }

        protected abstract object RunInternal(string input);
    }
}
