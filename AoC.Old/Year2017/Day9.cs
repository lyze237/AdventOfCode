using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day9 : Day
{
    public override object ExecutePart1()
    {
        var level = 0;
        var sumLevel = 0;
        var inGarbage = false;

        for (var i = 0; i < Input.Length; i++)
        {
            var c = Input[i];

            switch (c)
            {
                case '!':
                    i++;
                    break;

                case '{':
                    if (!inGarbage)
                    {
                        level++;
                    }

                    break;
                case '}':
                    if (!inGarbage)
                    {
                        sumLevel += level--;
                    }

                    break;

                case '<':
                    inGarbage = true;
                    break;
                case '>':
                    inGarbage = false;
                    break;
            }
        }

        return sumLevel;
    }

    public override object ExecutePart2()
    {
        var garbageCount = 0;
        var inGarbage = false;
            
        for (var i = 0; i < Input.Length; i++)
        {
            var c = Input[i];

            switch (c)
            {
                case '!':
                    i++;
                    break;
                        
                case '{':
                    break;
                case '}':
                    break;
                        
                case '<':
                    if (!inGarbage)
                        garbageCount--;
                    inGarbage = true;
                    break;
                case '>':
                    inGarbage = false;
                    break;
            }
                
            if (inGarbage && c != '!')
            {
                garbageCount++;
            }
        }
            
        return garbageCount;
    }
}