using System;
using AdventOfCodeLibrary.days;

namespace Day11
{
    public class Day11Section1 : TimeDay
    {
        public Day11Section1() : base(11, 1)
        {
        }

        protected override object RunInternal(string input)
        {
            int serialNumber = Convert.ToInt32(input);

            Grid maxGrid = null;
            for (int i = 1; i <= 298; i++) {
                for (int j = 1; j <= 298; j++) {
                    var currentGrind = new Grid(i, j, serialNumber, 3);
                    if (maxGrid == null || maxGrid.PowerLevel < currentGrind.PowerLevel)
                        maxGrid = currentGrind;
                }
            }
            
            return maxGrid?.ToString1();
        }
    }
}
