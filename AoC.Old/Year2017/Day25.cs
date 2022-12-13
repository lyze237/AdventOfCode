using System.Text.RegularExpressions;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day25 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        List<Task> allTasks = new List<Task> {new MoveTask(), new WriteTask(), new ContinueTask()};

        List<State> states = new List<State>();
        State currentState;
            
        Regex inStateRegex = new Regex(@"In state (\w+):");
        Regex ifCurrentValueRegex = new Regex(@"If the current value is (\d):");

        State stateToProcess = null;
        int currentIf = 0;            
        foreach (string l in Input)
        {
            string line = l.Trim();

            if (inStateRegex.IsMatch(line))
            {
                var name = inStateRegex.Match(line).Groups[1].Value;
                stateToProcess = new State(name);
                states.Add(stateToProcess);
            }
            else if (ifCurrentValueRegex.IsMatch(line))
            {
                currentIf = Convert.ToInt32(ifCurrentValueRegex.Match(line).Groups[1].Value);
            }
            else if (!string.IsNullOrEmpty(line))
            {
                foreach (var task in allTasks)
                {
                    if (task.Matches(line))
                    {
                        if (currentIf == 0)
                            stateToProcess?.If0Tasks.Add(line, task);
                        else
                            stateToProcess?.If1Tasks.Add(line, task);
                    }
                }
            }
        }

        currentState = states[0];

        Dictionary<int, int> values = new Dictionary<int, int>();
        int cursor = 0;
        for (int i = 0; i < 12_261_543; i++)
        {
            string next = currentState.Process(values, ref cursor);
            currentState = states.Find(state => state.Name == next);
        }

        return values.Count(pair => pair.Value == 1);
    }

    public override object ExecutePart2()
    {
        return -1;
    }

    private class State
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
    
    private class ContinueTask : Task
    {
        public ContinueTask() : base(@"- Continue with state ([A-Z]+).")
        {
        }

        public override void Run(string line, Dictionary<int, int> values, ref int cursor, out string nextState)
        {
            nextState = Regex.Match(line).Groups[1].Value;
        }
    }
    
    private class MoveTask : Task
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

    private class WriteTask : Task
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
    
    private abstract class Task
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