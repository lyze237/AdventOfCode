using System.Linq;
using System;
using System.Collections.Generic;
using Day18;
using Utils;

namespace Day23
{
    public class Day23Challenge1 : Challenge<long>
    {
        public Day23Challenge1() : base("input")
        {
        }

        public override long Run()
        {
            int muls = 0;
            List<Instruction> instructions = new List<Instruction>();

            foreach (var line in GetInputFilePerLine())
            {
                var strings = line.Split(' ');
                instructions.Add(new Instruction(strings));
            }
            
            for (int i = 0; i < instructions.Count; i++)
            {
                Instruction instruction = instructions[i];
                
                switch (instruction.Command)
                {
                    case "set":
                        instruction.LeftValue = instruction.RightValue;
                        break;
                    case "sub":
                        instruction.LeftValue -= instruction.RightValue;
                        break;
                    case "mul":
                        instruction.LeftValue *= instruction.RightValue;
                        muls++;
                        break;
                    case "jnz":
                        if (instruction.LeftValue != 0)
                        {
                            i += (int) instruction.RightValue - 1;

                            if (i < 0 || i >= instructions.Count)
                                return muls;
                        }
                        break;
                }
            }

            return muls;
        }
    }
}