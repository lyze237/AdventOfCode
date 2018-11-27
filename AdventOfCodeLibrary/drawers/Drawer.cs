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
    public abstract class Drawer
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }

        public ConsoleColor Foreground { get; set; }
        public ConsoleColor Background { get; set; }

        public bool Dirty { get; set; } = true;
        public bool Errored { get; set; }

        public bool DoneTick { get; set; }

        private bool done;
        public bool Done
        {
            get => done;
            set
            {
                done = value;
                DoneTick = true;
            }
        } 

        protected Drawer(int x, int y, int width, ConsoleColor foreground = ConsoleColor.Cyan, ConsoleColor background = ConsoleColor.Black)
        {
            X = x;
            Y = y;
            Width = width;

            Foreground = foreground;
            Background = background;        
        }

        public void Draw()
        {
            Dirty = false;
            DoneTick = false;

            lock (LockConsole.GetLock())
            {
                Console.ForegroundColor = Foreground;
                Console.BackgroundColor = Background;

                Console.SetCursorPosition(X, Y);

                DrawInternal();
            }
        }

        protected abstract void DrawInternal();

        public abstract void Tick();
    }
}
