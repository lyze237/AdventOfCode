using System.Collections.Generic;
using AdventOfCodeLibrary.days;

namespace Day5
{
    public class Day5Section1 : ProgressDay
    {
        public Day5Section1() : base(5, 1)
        {
        }

        protected override object RunInternal(string input)
        {
            var stack = new Stack<char>();

            ProgressBar.MaxValue = input.Length - 1;
           
            foreach (char c in input)
            {
                if (stack.Count == 0)
                {
                    stack.Push(c);
                    continue;
                }

                char inStack = stack.Peek();
                if (c != inStack && char.ToUpper(c) == char.ToUpper(inStack))
                    stack.Pop();
                else
                    stack.Push(c);

                ProgressBar.Value++;
            }

            return stack.Count;
        }
    }
}
