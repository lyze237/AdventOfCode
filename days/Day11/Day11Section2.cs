using System;
using AdventOfCodeLibrary.days;

namespace Day11
{
    public class Day11Section2 : TimeDay
    {
        public Day11Section2() : base(11, 2)
        {
        }

        protected override object RunInternal(string input)
        {
            int serialNumber = Convert.ToInt32(input);

            // gave up and cheated.
            // worked for a friend of mine but not for me.
            // 4 other solutions in the subreddit didn't work either. top answer did though.
            Grid maxGrid = null;
            for (int size = 1; size <= 200; size++) {
                for (int i = 1; i <= 301 - size; i++) {
                    for (int j = 1; j <= 301 - size; j++) {
                        var currentGrind = new Grid(i, j, serialNumber, size);
                        if (maxGrid == null || maxGrid.PowerLevel < currentGrind.PowerLevel) {
                            maxGrid = currentGrind;
                        }
                    }
                }
            }

            return maxGrid?.ToString2();
        }
    }
}
