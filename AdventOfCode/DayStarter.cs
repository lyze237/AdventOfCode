//-----------------------------------------------------------------------
// <copyright>
// MIT License
//
// Copyright (c) 2018 Michael Weinberger lyze@owl.sh
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeLibrary;
using AdventOfCodeLibrary.days;
using AdventOfCodeLibrary.drawers;

namespace AdventOfCode
{
    public class DayStarter
    {
        private List<Day> days;
        private DrawerManager drawerManager;
        private int width;

        public DayStarter(List<Day> days, int width = 80)
        {
            this.days = days;
            this.width = 80;

            drawerManager = new DrawerManager();
        }

        public void Start(params int[] whichDays)
        {
            var ourDays = days;
            if (whichDays.Length > 0)
                ourDays.RemoveAll(day => !whichDays.Contains(day.DayNumber));

            for (int index = 0, y = 1; index < days.Count; index++, y++)
            {
                var day = days[index];

                if (day.Section == days.Where(d => d.DayNumber == day.DayNumber).First().Section)
                {
                    string str = $"#    Day {day.DayNumber}    #";

                    if (day.DayNumber != days[0].DayNumber)
                    {
                        LockConsole.WriteLine();
                        LockConsole.WriteLine();
                        LockConsole.WriteLine();
                        LockConsole.WriteLine();
                    }
                    LockConsole.WriteLine(new string(' ', width / 2 - str.Length / 2) + new string('#', str.Length));
                    LockConsole.WriteLine(new string(' ', width / 2 - str.Length / 2) + str);
                    LockConsole.WriteLine(new string(' ', width / 2 - str.Length / 2) + new string('#', str.Length));
                    LockConsole.WriteLine();

                    if (day.DayNumber == days[0].DayNumber)
                        y += 4;
                    else
                        y += 8;
                }

                string sectionStr = $"Section {day.Section}: "; 
                LockConsole.WriteLine(sectionStr);

                day.SetupDrawer(sectionStr.Length + 2, y - 1, width - sectionStr.Length);

                drawerManager.Add(day.Drawer);

                StartDay(day);
            }

            drawerManager.Start();
        }


        private void StartDay(Day day)
        {
            Task.Run(() =>
            {
                day.Run(day.Input);
            });
        }
    }
}
