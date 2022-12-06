using System.Collections;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2022;

public class Day6 : Day
{
    public override object ExecutePart1() => 
        FindPacket(4);
    
    public override object ExecutePart2() => 
        FindPacket(14);

    private int FindPacket(int length)
    {
        var buffer = new Queue<char>(length + 1);

        for (var i = length; i < Input.Length; i++)
        {
            buffer.Enqueue(Input[i]);
            if (buffer.Count <= length) 
                continue;
            
            buffer.Dequeue();
                
            if (buffer.Distinct().Count() == buffer.Count)
                return i + 1;
        }

        return -1;
    }
}