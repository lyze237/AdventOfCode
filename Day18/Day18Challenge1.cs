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
            List<Instruction> instructions = new List<Instruction>();
            
            foreach (var line in GetInputFilePerLine())
            {
                var strings = line.Split(' ');
                
                if (strings.Length > 2)
                    instructions.Add(new Instruction(strings[0], strings[1], strings[2]));
                else
                    instructions.Add(new Instruction(strings[0], strings[1], string.Empty));
            }

            int fregency = 0;
            for (var i = 0; i < instructions.Count; i++)
            {
                var instruction = instructions[i];

                switch (instruction.Type)
                {
                    case "snd":
                        fregency = instruction.GetRegisterValue();
                        break;
                    case "set":
                        instruction.SetRegisterValue(instruction.GetInstructionValue());
                        break;
                    case "add":
                        instruction.SetRegisterValue(instruction.GetRegisterValue() + instruction.GetInstructionValue());
                        break;
                    case "mul":
                        instruction.SetRegisterValue(instruction.GetRegisterValue() * instruction.GetInstructionValue());
                        break;
                    case "mod":
                        instruction.SetRegisterValue(instruction.GetRegisterValue() % instruction.GetInstructionValue());
                        break;
                    case "rcv":
                        if (instruction.GetRegisterValue() > 0)
                            return fregency;
                        break;
                    case "jgz":
                        if (instruction.GetRegisterValue() > 0)
                            i += instruction.GetInstructionValue() - 1;
                        break;
                }
            }

            return -1;
        }
    }

    public class Instruction
    {
        public static Dictionary<string, int> Registers = new Dictionary<string, int>();
        
        public string Type { get; }
        private string register;
        private string value;

        public Instruction(string type, string register, string value)
        {
            Type = type;
            this.register = register;
            this.value = value;

            if (!Registers.ContainsKey(register))
                Registers.Add(register, 0);
        }

        public int GetRegisterValue()
        {
            return Registers[register];
        }

        public void SetRegisterValue(int v)
        {
            Registers[register] = v;
        }

        public int GetInstructionValue()
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return Registers[value];
            }
        }

        public override string ToString()
        {
            return $"{nameof(Type)}: {Type}, {nameof(register)}: {register}, {nameof(register)}.Value: {GetRegisterValue()}, {nameof(value)}: {value}";
        }
    }
}