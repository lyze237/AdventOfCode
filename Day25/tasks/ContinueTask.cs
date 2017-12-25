using System.Collections.Generic;

namespace Day25.tasks
{
    public class ContinueTask : Task
    {
        public ContinueTask() : base(@"- Continue with state ([A-Z]+).")
        {
        }

        public override void Run(string line, Dictionary<int, int> values, ref int cursor, out string nextState)
        {
            nextState = Regex.Match(line).Groups[1].Value;
        }
    }
}