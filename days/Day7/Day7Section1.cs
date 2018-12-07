using AdventOfCodeLibrary.days;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Day7
{
    public class Day7Section1 : TimeDay
    {
        public Day7Section1() : base(7, 1)
        {
        }

        protected override object RunInternal(string input)
        {
            var dependencies = new List<(char pre, char post)>();
            input.Split("\r\n").ToList().ForEach(x => dependencies.Add((x[5], x[36])));

            var allSteps = dependencies
                .Select(x => x.pre)
                .Concat(dependencies.Select(x => x.post))
                .Distinct()
                .OrderBy(x => x).ToList();

            string result = string.Empty;
            while (allSteps.Count > 0)
            {
                char valid = allSteps.First(s => dependencies.All(d => d.post != s));

                result += valid;

                allSteps.Remove(valid);
                dependencies.RemoveAll(d => d.pre == valid);
            }
            
            return result;
        }
    }
}
