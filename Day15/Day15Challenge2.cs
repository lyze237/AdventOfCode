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
using System.Collections.Generic;
using Utils;

namespace Day15
{
    public class Day15Challenge2 : Challenge<long>
    {
        public Day15Challenge2() : base("input")
        {
        }

        public override long Run()
        {
            var inputFilePerLine = GetInputFilePerLine();
            var genAValue = Values(Convert.ToInt64(inputFilePerLine[0].Split(' ').Last()), 16807, 4);
            var genBValue = Values(Convert.ToInt64(inputFilePerLine[1].Split(' ').Last()), 48271, 8);

            int judge = 0;
            for (long i = 0; i < 5_000_000; i++)
            {
                genAValue.MoveNext();
                genBValue.MoveNext();

                if (genAValue.Current == genBValue.Current)
                {
                    judge++;
                }
            }

            return judge;
        }

        private static IEnumerator<string> Values(long current, long factor, long modulo)
        {
            while (true)
            {
                current = current * factor % 2147483647;
                if (current % modulo == 0)
                    yield return string.Join("", Convert.ToString(current, 2).Reverse().Take(16));
            }
        }
    }
}
