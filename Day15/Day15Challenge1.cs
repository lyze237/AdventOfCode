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
using System.Runtime.CompilerServices;
using Utils;

namespace Day15
{
    public class Day15Challenge1 : Challenge<long>
    {
        public Day15Challenge1() : base("input")
        {
        }

        public override long Run()
        {
            var inputFilePerLine = GetInputFilePerLine();
            var genAValue = Convert.ToInt64(inputFilePerLine[0].Split(' ').Last());
            var genBValue = Convert.ToInt64(inputFilePerLine[1].Split(' ').Last());

            int judge = 0;
            for (long i = 0; i < 40_000_000; i++)
            {
                genAValue = CalculateANext(genAValue);
                genBValue = CalculateBNext(genBValue);

                string genA = Convert.ToString(genAValue, 2);
                string genB = Convert.ToString(genBValue, 2);

                if (string.Join("", genA.Reverse().Take(16)) == string.Join("", genB.Reverse().Take(16)))
                {
                    judge++;
                }
            }

            return judge;
        }

        private static long CalculateANext(long val)
        {
            val *= 16807;
            return val % 2147483647;
        }
        
        private static long CalculateBNext(long val)
        {
            val *= 48271;
            return val % 2147483647;
        }
    }
}
