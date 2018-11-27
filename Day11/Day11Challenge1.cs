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

using System.Linq;
using System;
using System.Drawing;
using Utils;

namespace Day11
{
    public class Day11Challenge1 : Challenge<int>
    {
        public Day11Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            Point origin = new Point(0, 0);
            Point coords = new Point(0, 0);
            
            foreach (var input in GetInputFile().Split(','))
            {
                coords = Move(input, coords);
            }

            return GetDistance(origin, coords);
        }
        
        private static Point Move(string d, Point position)
        {
            switch (d)
            {
                case "n":
                    return new Point(position.X, position.Y + 2);
                case "s":
                    return new Point(position.X, position.Y - 2);
                    
                case "ne":
                    return new Point(position.X + 1, position.Y + 1);
                case "nw":
                    return new Point(position.X - 1, position.Y + 1);
                    
                case "se":
                    return new Point(position.X + 1, position.Y - 1);
                case "sw":
                    return new Point(position.X - 1, position.Y - 1);
                    
                default:
                    throw new ArgumentException(d);
            }
        }
        
        private static int GetDistance(Point origin, Point position)
        {
            var xM = Math.Abs(position.X - origin.X);
            var yM = (Math.Abs(position.Y - origin.Y) - xM) / 2;

            return xM + yM;
        }
    }
}
