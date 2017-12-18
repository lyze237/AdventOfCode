using System.Linq;
using System;
using System.Collections.Generic;
using Utils;

namespace Day18
{
    public class Day18Challenge1 : Challenge<int>
    {
        public Day18Challenge1() : base("input")
        {
        }

        public override int Run()
        {
            int frequency = 0;

            List<Instruction> instructions = new List<Instruction>();

            foreach (var line in GetInputFilePerLine())
            {
                var strings = line.Split(' ');
                instructions.Add(new Instruction(strings));
            }
            
            for (var i = 0;; i++)
            {
                Instruction instruction = instructions[i];
                
                switch (instruction.Command)
                {
                    case "snd":
                        frequency = instruction.RegisterValue;
                        break;
                    case "set":
                        instruction.RegisterValue = instruction.Value;
                        break;
                    case "add":
                        instruction.RegisterValue += instruction.Value;
                        break;
                    case "mul":
                        instruction.RegisterValue *= instruction.Value;
                        break;
                    case "mod":
                        instruction.RegisterValue %= instruction.Value;
                        break;
                    case "rcv":
                        if (instruction.RegisterValue != 0)
                            return frequency;
                        break;
                    case "jgz":
                        if (instruction.RegisterValue > 0)
                        {
                            i += instruction.Value - 1;

                            if (i < 0 || i >= instructions.Count)
                                return -1;
                        }
                        break;
                }
            }
        }
    }
}