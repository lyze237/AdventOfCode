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
    public class ProgressBar : Drawer
    {
        #region Fields

        private long minValue;
        private long value;
        private long maxValue;

        #endregion

        #region Properties

        public long MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                Dirty = true;
            }
        }

        public long Value
        {
            get => value;
            set
            {
                this.value = value;
                Dirty = true;
            }
        }

        public long MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                Dirty = true;
            }
        }

        #endregion

        #region Constructors

        public ProgressBar(int x, int y, int width) : this (x, y, width, 0, 0, 100)
        {
            
        }

        public ProgressBar(int x, int y, int width, long value, long minValue, long maxValue) : base(x, y, width)
        {
            MinValue = minValue;
            Value = value;
            MaxValue = maxValue;
        }


        #endregion

        protected override void DrawInternal()
        {
            var newWidth = Width - 2;
            var newValue = (Value - MinValue) * newWidth / (MaxValue - MinValue);

            Console.Write('[');
            if (newValue - 1 > 0)
                Console.Write(new string('=', (int) (newValue - 1)));
            Console.Write('>');
            if (Width - newValue > 0)
                Console.Write(new string(' ', (int) (newValue == 0 ? newWidth - 1 : newWidth - newValue)));
            Console.Write(']');
        }

        public override void Tick()
        {
            if (DoneTick)
            {
                Foreground = Value != MaxValue || Errored ? ConsoleColor.Red : ConsoleColor.Green;
                Dirty = true;
            }
        }
    }
}
