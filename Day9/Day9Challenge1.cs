using System;
using Utils;

namespace Day9
{
    public class Day9Challenge1 : Challenge<int>
    {
        public Day9Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            string input = GetInputFile();

            int level = 0;
            int sumLevel = 0;
            bool inGarbage = false;
            
            for (var i = 0; i < input.Length; i++)
            {
                char c = input[i];


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
    }
}