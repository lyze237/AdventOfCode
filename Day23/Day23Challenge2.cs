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

            long b = 99 * 100 + 100000;
            long c = b + 17000;
            
            while (true)
            {
                bool f = false;
                
                for (int d = 2; d < b; d++)
                {
                    if (b % d == 0)
                    {
                        f = true;
                        break;
                    }
                }

                if (f)
                    h++;

                if (b == c)
                    return h;

                b += 17;
            }
        }
    }
}