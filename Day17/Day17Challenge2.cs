using System.Linq;
using System;
using System.Collections.Generic;
using Utils;

namespace Day17
{
    public class Day17Challenge2 : Challenge<int>
    {
        public Day17Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            int stepsToMove = Convert.ToInt32(GetInputFile());
            Console.WriteLine(stepsToMove);
            
            int currentPosition = 0;
            int result = 0;

            for (int i = 0; i < 50_000_000; i++)
            {
                currentPosition = (currentPosition + stepsToMove  % (i+1)) % (i+1);
                if (currentPosition++ == 0)
                {
                    result = i + 1;
                }
            }
            
            return result;
        }
    }
}