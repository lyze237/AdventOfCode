using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day5 : Day
{
    public override object ExecutePart1()
    {
        var stack = new Stack<char>();

        foreach (var c in Input)
        {
            if (stack.Count == 0)
            {
                stack.Push(c);
                continue;
            }

            var inStack = stack.Peek();
            if (c != inStack && char.ToUpper(c) == char.ToUpper(inStack))
                stack.Pop();
            else
                stack.Push(c);
        }

        return stack.Count;
    }

    public override object ExecutePart2()
    {
        var result = new Dictionary<char, int>();
        for (int i = 'a'; i <= 'z'; i++)
        {
            var stack = new Stack<char>();
                    
            var currentInput = new string(Input);
            currentInput = currentInput.Replace(((char) i).ToString(), "").Replace(char.ToUpper((char) i).ToString(), "");
                
            foreach (var c in currentInput)
            {
                if (stack.Count == 0)
                {
                    stack.Push(c);
                    continue;
                }

                var inStack = stack.Peek();
                if (c != inStack && char.ToUpper(c) == char.ToUpper(inStack))
                    stack.Pop();
                else
                    stack.Push(c);
            }
                
            result.Add((char) i, stack.Count);
        }
            
        return result.MinBy(r => r.Value).Value;
    }
}