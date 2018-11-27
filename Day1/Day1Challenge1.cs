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

namespace Day1
{
    class Day1Challenge1 : Challenge<int>
    {
        public Day1Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            string input = GetInputFile();

            int sum = 0;
            var charArray = input.ToCharArray();
            for (int i = 1; i < charArray.Length; i++)
            {
                int left = (int) Char.GetNumericValue(charArray[i - 1]);
                int right = (int) Char.GetNumericValue(charArray[i]);

                if (left == right) {
                    sum += left;   
                    System.Console.WriteLine(left + " = " + right);
                }

            }

            int left2 = (int) Char.GetNumericValue(charArray[0]);
            int right2 = (int) Char.GetNumericValue(charArray[charArray.Length - 1]);

            if (left2 == right2) {
                sum += left2;
            }

            return sum;
        }
    }
}
