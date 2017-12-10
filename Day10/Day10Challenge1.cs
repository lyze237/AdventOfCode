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
