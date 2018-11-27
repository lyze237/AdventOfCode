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
using Utils;

namespace Day18
{
    public class Day18Challenge1 : Challenge<long>
    {
        public Day18Challenge1() : base("input")
        {
        }

        public override long Run()
        {
            long frequency = 0;

            List<Instruction> instructions = new List<Instruction>();

            foreach (var line in GetInputFilePerLine())
            {
                var strings = line.Split(' ');
                instructions.Add(new Instruction(strings));
            }
            
            for (int i = 0;; i++)
            {
                Instruction instruction = instructions[i];
                
                switch (instruction.Command)
                {
                    case "snd":
                        frequency = instruction.LeftValue;
                        Console.WriteLine($"Playing {frequency}");
                        break;
                    case "set":
                        instruction.LeftValue = instruction.RightValue;
                        break;
                    case "add":
                        instruction.LeftValue += instruction.RightValue;
                        break;
                    case "mul":
                        instruction.LeftValue *= instruction.RightValue;
                        break;
                    case "mod":
                        instruction.LeftValue %= instruction.RightValue;
                        break;
                    case "rcv":
                        if (instruction.LeftValue != 0)
                            return frequency;
                        break;
                    case "jgz":
                        if (instruction.LeftValue > 0)
                        {
                            i += (int) instruction.RightValue - 1;

                            if (i < 0 || i >= instructions.Count)
                                return -1;
                        }
                        break;
                }
            }
        }
    }
}
