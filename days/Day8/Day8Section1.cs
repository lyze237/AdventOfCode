using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLibrary.days;

namespace Day8
{
    public class Day8Section1 : TimeDay
    {
        public Day8Section1() : base(8, 1)
        {
        }

        protected override object RunInternal(string input)
        {
            var numbers = input.Split(" ").Select(int.Parse).ToList();

            var i = 0;
            var node = Node.GetNode(numbers, ref i);

            return node.AllSum;
        }

    }
}
