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

using System.Linq;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text.RegularExpressions;
using Day25.tasks;
using Utils;

namespace Day25
{
    public class Day25Challenge1 : Challenge<int>
    {
        public Day25Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            List<Task> allTasks = new List<Task> {new MoveTask(), new WriteTask(), new ContinueTask()};

            List<State> states = new List<State>();
            State currentState;
            
            Regex inStateRegex = new Regex(@"In state (\w+):");
            Regex ifCurrentValueRegex = new Regex(@"If the current value is (\d):");

            State stateToProcess = null;
            int currentIf = 0;            
            foreach (string l in GetInputFilePerLine())
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
    }
}
