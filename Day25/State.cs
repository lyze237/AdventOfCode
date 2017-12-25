using System.Collections.Generic;
using Day25.tasks;

namespace Day25
{
    public class State
    {
        public string Name { get; set; }

        public Dictionary<string, Task> If1Tasks { get; set; } = new Dictionary<string, Task>();
        
        public Dictionary<string, Task> If0Tasks { get; set; } = new Dictionary<string, Task>();

        public State(string name)
        {
            Name = name;
        }

        public string Process(Dictionary<int, int> values, ref int cursor)
        {
            if (values.ContainsKey(cursor) && values[cursor] == 1)
            {
                return Process(values, ref cursor, If1Tasks);
            }
            return Process(values, ref cursor, If0Tasks);
        }

        private string Process(Dictionary<int, int> values, ref int cursor, Dictionary<string, Task> tasks)
        {
            foreach (var task in tasks)
            {
                task.Value.Run(task.Key, values, ref cursor, out string nextState);

                if (nextState != null)
                {
                    return nextState;
                }
            }

            return null;
        }
    }
}