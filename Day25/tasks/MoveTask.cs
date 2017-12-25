using System.Collections.Generic;

namespace Day25.tasks
{
    public class MoveTask : Task
    {
        public MoveTask() : base(@"- Move one slot to the ([a-z]+).")
        {
        }

        public override void Run(string line, Dictionary<int, int> values, ref int cursor, out string nextState)
        {
            if (Regex.Match(line).Groups[1].Value == "left")
                cursor--;
            else
                cursor++;
            
            nextState = null;
        }
    }
}