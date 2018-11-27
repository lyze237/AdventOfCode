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
