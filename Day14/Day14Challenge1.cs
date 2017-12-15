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