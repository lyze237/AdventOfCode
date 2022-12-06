using Tidy.AdventOfCode;

namespace AdventOfCode.Year2022;

public class Day6 : Day
{
    public override object ExecutePart1() => 
        FindPacket2(4);
    
    public override object ExecutePart2() => 
        FindPacket2(14);

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
    
    private int FindPacket2(int length)
    {
        for (var i = length; i < Input.Length; i++)
            if (Input.Substring(i - length, length).Distinct().Count() == length)
                return i;

        return -1;
    }
}