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
using System.Linq;
using Utils;

namespace Day10
{
    public class Day10Challenge1 : Challenge<int>
    {
        public Day10Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            int[] lengths = GetInputFile().Split(',').Select(s => Convert.ToInt32(s)).ToArray();
            int[] tmpList = new int[256];
            for (int i = 0; i < tmpList.Length; i++)
            {
                tmpList[i] = i;
            }
            WrapArray<int> list = new WrapArray<int>(tmpList);

            int cursor = 0;

            Console.WriteLine($"L: {string.Join(", ", lengths)}");
            
            for (int i = 0; i < lengths.Length; i++)
            {
                Console.WriteLine($"B {i}: {string.Join(", ", tmpList)}");
                
                for (int startIndex = cursor; startIndex < cursor + lengths[i] / 2; startIndex++)
                {
                    int temp = list[cursor + lengths[i] - 1 - (startIndex - cursor)];
                    list[cursor + lengths[i] - 1 - (startIndex - cursor)] = list[startIndex];
                    list[startIndex] = temp;
                }

                cursor += lengths[i] + i;
                Console.WriteLine($"A {i}: {string.Join(", ", tmpList)}\nC: {cursor % list.Length}\n");
            }
            
            
            return tmpList[0] * tmpList[1];
        }
    }
}
