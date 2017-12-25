using System.Collections.Generic;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Day25.tasks
{
    public abstract class Task
    {
        public Regex Regex { get; set; }
        
        protected Task(string regex)
        {
            Regex = new Regex(regex);
        }

        public abstract void Run(string line, Dictionary<int, int> values, ref int cursor, out string nextState);

        public bool Matches(string line)
        {
            return Regex.IsMatch(line);
        }
    }
}