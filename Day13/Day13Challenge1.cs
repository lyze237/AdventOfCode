using System.Linq;
using System;
using Utils;

namespace Day13
{
    public class Day13Challenge1 : Challenge<int>
    {
        public Day13Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            
            var layers = GetInputFilePerLine()
                .Select(line => line.Split(':'))
                .Select(splitted =>
                    new Layer(Convert.ToInt32(splitted[0].Trim()), Convert.ToInt32(splitted[1].Trim()))).ToList();

            int caughtAmount = 0;

            int maxLayerCount = layers.Max(l => l.Position);
            for (int i = 0; i <= maxLayerCount; i++)
            {
                var layer = layers.FirstOrDefault(l => l.Position == i);
                if (layer?.ScannerDepth == 0)
                {
                    caughtAmount += layer.GetSeverity();
                }
                layers.ForEach(l => l.MoveScanner());
            }
            
            return caughtAmount;
        }
    }
}