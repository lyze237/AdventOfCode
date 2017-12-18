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