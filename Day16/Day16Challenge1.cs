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
using System.IO.IsolatedStorage;
using System.Text;
using Utils;

namespace Day16
{
    public class Day16Challenge1 : Challenge<string>
    {
        public Day16Challenge1() : base("input")
        {
        }

        public override string Run()
        {
            string programs = "abcdefghijklmnop";
            
            foreach (var task in GetInputFile().Split(','))
            {
                var parameter = task.Substring(1);
                switch (task[0])
                {
                    case 's':
                        int amount = Convert.ToInt32(parameter);
                        
                        var temp = programs.Substring(programs.Length - amount);
                        programs = programs.Remove(programs.Length - amount);
                        
                        programs = temp + programs;
                        break;
                    case 'x':
                        var strings = parameter.Split('/');
                        
                        var indexA = Convert.ToInt32(strings[0]);
                        var indexB = Convert.ToInt32(strings[1]);

                        var programsArray = programs.ToCharArray();
                        programsArray[indexA] = programs[indexB];
                        programsArray[indexB] = programs[indexA];

                        programs = string.Join(string.Empty, programsArray);
                        break;
                    case 'p':
                        strings = parameter.Split('/');

                        var programA = strings[0];
                        var programB = strings[1];

                        indexA = programs.IndexOf(programA, StringComparison.Ordinal);
                        indexB = programs.IndexOf(programB, StringComparison.Ordinal);
                        
                        programsArray = programs.ToCharArray();
                        programsArray[indexA] = programs[indexB];
                        programsArray[indexB] = programs[indexA];

                        programs = string.Join(string.Empty, programsArray); 
                        break;
                }
            }

            return programs;
        }
    }
}
