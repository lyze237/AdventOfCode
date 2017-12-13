using System.Linq;
using System;
using Utils;

namespace Day13
{
    public class Day13Challenge2 : Challenge<int>
    {
        public Day13Challenge2() : base("input")
        {
        }

        public override int Run()
        {
            bool caught;
            int delay = 0;

            var layers = GetInputFilePerLine()
                .Select(line => line.Split(':'))
                .Select(splitted =>
                    new Layer(Convert.ToInt32(splitted[0].Trim()), Convert.ToInt32(splitted[1].Trim()))).ToList();

            do
            {
                caught = false;          

                int maxLayerCount = layers.Max(l => l.Position);
                
                for (int i = 0; i <= maxLayerCount; i++)
                {
                    var layer = layers.FirstOrDefault(l => l.Position == i);
                    if (layer?.IsCaught(delay + i) == true)
                    {
                        caught = true;
                        delay++;
                        break;
                    }
                }
            } while (caught);
            
            return delay;
        }
    }
}