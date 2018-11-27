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
using System.Linq;
using System.Text.RegularExpressions;
 using Utils;

namespace Day8
{
    public class Day8Challenge1 : Challenge<int>
    {
        public Day8Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            List<Instruction> instructions = new List<Instruction>();
            Dictionary<string, int> registers = new Dictionary<string, int>();
            int maxValue = int.MinValue; 
            Regex regex = new Regex(@"(\w+) (dec|inc) (-?\d+) if (\w+) ([<=>!]{1,2}) (-?\d+)");
            foreach (string line in GetInputFilePerLine())
            {
                Match match = regex.Match(line);
                instructions.Add(new Instruction(match.Groups[1].Value, match.Groups[4].Value,
                    int.Parse(match.Groups[3].Value), match.Groups[2].Value.Equals("inc"),
                    match.Groups[5].Value, int.Parse(match.Groups[6].Value)));
                registers[match.Groups[1].Value] = 0;
            }

            foreach (Instruction instruction in instructions)
            {
                switch (instruction.Condition)
                {
                    case ">":
                        if (registers[instruction.OnWho] > instruction.Amount)
                            registers[instruction.Name] =
                                instruction.IsInc
                                    ? registers[instruction.Name] + instruction.ChangeAmount
                                    : registers[instruction.Name] - instruction.ChangeAmount;
                        break;
                    case "<":
                        if (registers[instruction.OnWho] < instruction.Amount)
                            registers[instruction.Name] =
                                instruction.IsInc
                                    ? registers[instruction.Name] + instruction.ChangeAmount
                                    : registers[instruction.Name] - instruction.ChangeAmount;
                        break;
                    case "<=":
                        if (registers[instruction.OnWho] <= instruction.Amount)
                            registers[instruction.Name] =
                                instruction.IsInc
                                    ? registers[instruction.Name] + instruction.ChangeAmount
                                    : registers[instruction.Name] - instruction.ChangeAmount;
                        break;
                    case ">=":
                        if (registers[instruction.OnWho] >= instruction.Amount)
                            registers[instruction.Name] =
                                instruction.IsInc
                                    ? registers[instruction.Name] + instruction.ChangeAmount
                                    : registers[instruction.Name] - instruction.ChangeAmount;
                        break;
                    case "==":
                        if (registers[instruction.OnWho] == instruction.Amount)
                            registers[instruction.Name] =
                                instruction.IsInc
                                    ? registers[instruction.Name] + instruction.ChangeAmount
                                    : registers[instruction.Name] - instruction.ChangeAmount;
                        break;
                    case "!=":
                        if (registers[instruction.OnWho] != instruction.Amount)
                            registers[instruction.Name] =
                                instruction.IsInc
                                    ? registers[instruction.Name] + instruction.ChangeAmount
                                    : registers[instruction.Name] - instruction.ChangeAmount;
                        break;
                }
                
            }
            return registers.Max(pair => pair.Value);
        }
    }
}
