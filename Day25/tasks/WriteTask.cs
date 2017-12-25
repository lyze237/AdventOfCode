using System;
using System.Collections.Generic;

namespace Day25.tasks
{
    public class WriteTask : Task
    {
        public WriteTask() : base(@"- Write the value (\d).")
        {
        }

        public override void Run(string line, Dictionary<int, int> values, ref int cursor, out string nextState)
        {
            int valueToWrite = Convert.ToInt32(Regex.Match(line).Groups[1].Value);
            values[cursor] = valueToWrite;
            
            nextState = null;
        }
    }
}