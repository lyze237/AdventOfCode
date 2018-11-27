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
using Day10;
using Utils;

namespace Day14
{
    public class Day14Challenge1 : Challenge<int>
    {
        public Day14Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            var input = GetInputFile();

            int set = 0;
            List<List<int>> knotField = new List<List<int>>();
            for (int i = 0; i < 128; i++)
            {
                var knot = CalculateKnot($"{input}-{i}");
                
                string binaryKnot = string.Join(string.Empty,
                    knot.Select(
                        c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                    )
                );
                Console.WriteLine(binaryKnot);
                set += binaryKnot.Count(c => c == '1');
            }
            return set;
        }

        /// <summary>
        /// Copy pasted from Day10Challenge2
        /// </summary>
        /// <param name="input">The input "file"</param>
        /// <returns>The calculated knot hash</returns>
        private static string CalculateKnot(string input)
        {
            int[] lengths = input.ToCharArray().Select(c => (int) c)
                .Append(17).Append(31).Append(73).Append(47).Append(23)
                .ToArray();
            int[] tmpList = new int[256];
            for (int i = 0; i < tmpList.Length; i++)
            {
                tmpList[i] = i;
            }
            WrapArray<int> list = new WrapArray<int>(tmpList);

            int cursor = 0;
            int skipSize = 0;

            for (int z = 0; z < 64; z++)
            {
                for (int i = 0; i < lengths.Length; i++, skipSize++)
                {
                    for (int startIndex = cursor; startIndex < cursor + lengths[i] / 2; startIndex++)
                    {
                        int temp = list[cursor + lengths[i] - 1 - (startIndex - cursor)];
                        list[cursor + lengths[i] - 1 - (startIndex - cursor)] = list[startIndex];
                        list[startIndex] = temp;
                    }

                    cursor += lengths[i] + skipSize;
                }
            }

            List<int> xorList = new List<int>();
            for (int i = 0; i < list.Length; i += 16)
            {
                int[] toXor = list.Array.Skip(i).Take(16).ToArray();
                int xorResult = toXor[0];

                for (int j = 1; j < toXor.Length; j++)
                {
                    xorResult ^= toXor[j];
                }
                xorList.Add(xorResult);
            }

            string ret = "";
            foreach (int element in xorList)
            {
                ret += element.ToString("X").PadLeft(2, '0');
            }
                
            return ret.ToLower();
        }
    }
}
