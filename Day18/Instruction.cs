using System;
using System.Collections.Generic;

namespace Day18
{
    public class Instruction
    {
        private static Dictionary<string, int> registers = new Dictionary<string, int>();
        
        public string Command { get; }
        public string Register { get; }
        private string value;

        public int Value
        {
            get
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Value is not set");
                try
                {
                    return Convert.ToInt32(value);
                }
                catch (FormatException)
                {
                    return registers[value];
                }
            }
        }

        public int RegisterValue
        {
            get => registers[Register];
            set => registers[Register] = value;
        }
        
        public Instruction(string[] strings)
        {
            Command = strings[0];
            Register = strings[1];
            
            if (!registers.ContainsKey(Register))
                registers.Add(Register, 0);
            
            if (strings.Length > 2)
                value = strings[2];    
        }

        public override string ToString()
        {
            return $"{nameof(Command)}: {Command}, {nameof(Register)}: {Register}, {nameof(value)}: {value}";
        }
    }
}