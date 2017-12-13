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
                layers.ForEach(l => l.Reset());                

                int maxLayerCount = layers.Max(l => l.Position);
                for (int i = 0; i < delay; i++)
                {
                    layers.ForEach(l => l.MoveScanner());
                }
                
                for (int i = 0; i <= maxLayerCount; i++)
                {
                    var layer = layers.FirstOrDefault(l => l.Position == i);
                    if (layer?.ScannerDepth == 0)
                    {
                        caught = true;
                        delay++;
                        break;
                    }
                    layers.ForEach(l => l.MoveScanner());
                }
            } while (caught);
            
            return delay;
        }
    }
}