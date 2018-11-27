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
    public class PercentProgressBar : ProgressBar
    {
        #region Properties

        public ConsoleColor Text { get; set; }

        #endregion

        #region Constructors

        public PercentProgressBar(int x, int y, int width) : this (x, y, width, 0, 0, 100)
        {
            
        }

        public PercentProgressBar(int x, int y, int width, int value, int minValue, int maxValue) 
            : base(x, y, width, value, minValue, maxValue)
        {
            Text = ConsoleColor.White;
        }


        #endregion

        protected override void DrawInternal()
        {
            base.DrawInternal();

            var percent = (Value - MinValue) * 100 / (MaxValue - MinValue);
            var str = $"{percent} %";

            Console.ForegroundColor = Text;
            Console.SetCursorPosition(X + Width / 2 - str.Length / 2, Y);
            Console.Write(str);
        }
    }
}
