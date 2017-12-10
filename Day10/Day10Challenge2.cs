using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Day10
{
    public class Day10Challenge2 : Challenge<string>
    {
        public Day10Challenge2() : base("input")
        {
        }

        public override string Run()
        {
            int[] lengths = GetInputFile().ToCharArray().Select(c => (int) c)
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