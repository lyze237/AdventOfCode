using System.Linq;
using System;
using System.Collections.Generic;
using Utils;

namespace Day17
{
    public class Day17Challenge1 : Challenge<int>
    {
        public Day17Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            int stepsToMove = Convert.ToInt32(GetInputFile());
            List<int> buffer = new List<int> {0};
            
            int currentPosition = 0;

            for (int i = 1; i <= 2017; i++)
            {
                currentPosition = (currentPosition + stepsToMove % buffer.Count) % buffer.Count;
                buffer.Insert(++currentPosition, i);
            }
            
            return buffer[buffer.IndexOf(2017) + 1];
        }
    }
}