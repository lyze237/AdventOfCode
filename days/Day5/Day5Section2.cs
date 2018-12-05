using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AdventOfCodeLibrary.days;
using AdventOfCodeLibrary.drawers;

namespace Day5
{
    public class Day5Section2 : ProgressDay
    {
        public Day5Section2() : base(5, 2)
        {
        }

        protected override object RunInternal(string input)
        {
            ProgressBar.MaxValue = (input.Length - 1) * ('z' - 'a');

            var result = new Dictionary<char, int>();
            for (int i = 'a'; i <= 'z'; i++)
            {
                var stack = new Stack<char>();
                    
                var currentInput = new string(input);
                currentInput = currentInput.Replace(((char) i).ToString(), "").Replace(char.ToUpper((char) i).ToString(), "");
                
                foreach (char c in currentInput)
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
                
                result.Add((char) i, stack.Count);
            }
            
            return result.OrderBy(r => r.Value).First().Value;
        }
    }
}
