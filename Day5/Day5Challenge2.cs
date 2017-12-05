using System;
using System.Linq;
using Utils;

namespace Day5
{
    public class Day5Challenge2 : Challenge
    {
        public Day5Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            int[] instructions = GetInputFilePerLine().Select(line => Convert.ToInt32(line)).ToArray();

            int pointerIndex = 0;
            int jumps = 0;

            for (; pointerIndex >= 0 && pointerIndex < instructions.Length; jumps++)
            {
                int instruction = instructions[pointerIndex];

                if (instruction >= 3)
                    instructions[pointerIndex]--;
                else
                    instructions[pointerIndex]++;

                pointerIndex += instruction;
            }

            return jumps;
        }
    }
}