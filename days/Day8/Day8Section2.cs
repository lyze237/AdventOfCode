using System.Linq;
using AdventOfCodeLibrary.days;

namespace Day8
{
    public class Day8Section2 : TimeDay
    {
        public Day8Section2() : base(8, 2)
        {
        }

        protected override object RunInternal(string input)
        {
            var numbers = input.Split(" ").Select(int.Parse).ToList();

            var i = 0;
            var node = Node.GetNode(numbers, ref i);

            return node.Value;
        }
    }
}
