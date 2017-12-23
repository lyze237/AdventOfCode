using System.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.NetworkInformation;
using Day18;
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
                long d = 2;

                do
                {
                    long e = 2;
                    do
                    {
                        if (d * e == b)
                            f = true;

                        e++;
                    } while (e != b);

                    d++;
                } while (d != b);

                if (f)
                    h++;

                if (b == c)
                    return h;

                b += 17;
            }
        }
    }
}