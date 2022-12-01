using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day5 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var instructions = Input.Select(line => Convert.ToInt32(line)).ToArray();

        var pointerIndex = 0;
        var jumps = 0;

        for (;pointerIndex >= 0 && pointerIndex < instructions.Length; jumps++)
        {
            var instruction = instructions[pointerIndex];
                
            instructions[pointerIndex]++;

            pointerIndex += instruction;
        }
            
        return jumps;
    }

    public override object ExecutePart2()
    {
        var instructions = Input.Select(line => Convert.ToInt32(line)).ToArray();

        var pointerIndex = 0;
        var jumps = 0;

        for (; pointerIndex >= 0 && pointerIndex < instructions.Length; jumps++)
        {
            var instruction = instructions[pointerIndex];

            if (instruction >= 3)
                instructions[pointerIndex]--;
            else
                instructions[pointerIndex]++;

            pointerIndex += instruction;
        }

        return jumps;
    }
}