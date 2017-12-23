using System;
using Utils;

namespace Day23
{
    public class Day23Challenge2 : Challenge<long>
    {
        public Day23Challenge2() : base("input")
        {
        }

        public override long Run()
        {
            long h = 0;

            long b = Convert.ToInt32(GetInputFilePerLine()[0].Replace("set b ", ""));
            b = b * 100 + 100000;
            long c = b + 17000;
            
            for (; b != c; b += 17)
            {
                for (int d = 2; d < b; d++)
                {
                    if (b % d == 0)
                    {
                        h++;
                        break;
                    }
                }
            }
            return h + 1;
        }
    }
}