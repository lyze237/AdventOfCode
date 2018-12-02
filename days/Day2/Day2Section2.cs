using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventOfCodeLibrary.days;

namespace Day2
{
    public class Day2Section2 : ProgressDay
    {
        public Day2Section2() : base(2, 2)
        {
        }

        protected override object RunInternal(string input)
        {
            var lines = input.Split("\n");

            ProgressBar.MaxValue = lines.Length - 1;

            for (var index = 0; index < lines.Length; index++)
            {
                string box = lines[index];


                foreach (string compareBox in lines)
                {
                    string sames = box
                        .Where((c, i) => c == compareBox[i])
                        .Aggregate("", (current, t) => current + t);

                    if (sames.Length == box.Length - 1)
                        return sames;
                }
                    
                Update(index);
            }
            
            return null;
        }
    }
}
