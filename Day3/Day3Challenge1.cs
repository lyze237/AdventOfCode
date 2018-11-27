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
using Utils;

namespace Day3
{
    public class Day3Challenge1 : Challenge<int>
    {
        public Day3Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            int input = Convert.ToInt32(GetInputFile());

            int ring = 0;
            for (int maxRingVal = 1; maxRingVal < input; maxRingVal += 8 * ++ring)
            {
            }
            Console.WriteLine("Ring: " + ring);
            int sequenceIndex = (input - 1) % (2 * ring);
            int distAlongEdge = Math.Abs(sequenceIndex - ring);
            int taxiDist = distAlongEdge + ring;

            return taxiDist;
        }
    }
}
