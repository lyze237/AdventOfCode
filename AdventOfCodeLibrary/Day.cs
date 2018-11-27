using System;

namespace AdventOfCodeLibrary
{
    public abstract class Day
    {
        public int DayNumber { get; set; }
        public int Section { get; set; }
        public DayType Type { get; set; }

        public Day(int dayNumber, int section, DayType type)
        {
            DayNumber = dayNumber;
            Section = section;
            Type = type;
        }
    }
}
