using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLibrary.days;

namespace Day7
{
    public class Day7Section2 : TimeDay
    {
        public Day7Section2() : base(7, 2)
        {
        }

        protected override object RunInternal(string input)
        {            
            var deps = new List<(char Pre, char Post)>();

            input.Split("\r\n").ToList().ForEach(x => deps.Add((x[5], x[36])));

            var allSteps = deps
                .Select(d => d.Pre)
                .Concat(deps.Select(d => d.Post))
                .Distinct()
                .OrderBy(d => d)
                .ToList();
            
            var workers = new List<int>(5) { 0, 0, 0, 0, 0 };
            var currentSecond = 0;
            var doneList = new List<(char step, int finish)>();

            while (allSteps.Count > 0 || workers.Count(w => w > currentSecond) > 0)
            {
                foreach (var x in doneList.Where(d => d.finish <= currentSecond).ToList())
                {
                    deps.RemoveAll(d => d.Pre == x.step);
                    doneList.Remove(x);
                }

                var valid = allSteps.Where(s => deps.All(d => d.Post != s)).ToList();

                for (var w = 0; w < workers.Count(x => x <= currentSecond) && valid.Count > 0; w++)
                {
                    workers[w] = valid.First() - 'A' + 61 + currentSecond;
                    allSteps.Remove(valid.First());
                    doneList.Add((valid.First(), workers[w]));
                    valid.RemoveAt(0);
                }

                currentSecond++;
            }

            return currentSecond;
        }
    }
}
