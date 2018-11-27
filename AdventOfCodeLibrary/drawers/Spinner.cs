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
    public class Spinner : Drawer
    {
        #region Fields

        private int value;

        private bool right;

        private int tickCount = 0;
        public int UpdatesAfterTicks = 1;

        #endregion

        #region Constructors

        public Spinner(int x, int y, int width) : base(x, y, width)
        {
        }

        #endregion

        protected override void DrawInternal()
        {
            var newWidth = Width - 2;
            var newValue = (((value - 0) * (newWidth - 0)) / (Width - 0)) + 0;

            Console.Write('[');
            if (newValue - 1 > 0)
                Console.Write(new string(' ', newValue - 1));
            Console.Write('=');
            if (Width - newValue > 0)
                Console.Write(new string(' ', newValue == 0 ? newWidth - 1 : newWidth - newValue));
            Console.Write(']');
        }

        public override void Tick()
        {
            if (DoneTick)
                Foreground = Errored ? ConsoleColor.Red : ConsoleColor.Green;

            if (++tickCount < UpdatesAfterTicks)
                return;

            tickCount = 0;

            if (right)
            {
                if (++value > Width)
                {
                    right = false;
                    value = Width;
                }
            }
            else
            {
                if (--value < 0)
                {
                    right = true;
                    value = 0;
                }
            }

            Dirty = true;
        }
    }
}
