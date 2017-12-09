using Utils;

namespace Day9
{
    public class Day9Challenge2 : Challenge<int>
    {
        public Day9Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            string input = GetInputFile();

            int garbageCount = 0;
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
}