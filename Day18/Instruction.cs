using System;
using System.Collections.Generic;

namespace Day18
{
    public class Instruction
    {
        private static Dictionary<string, long> registers = new Dictionary<string, long>();
        
        public string Command { get; }
        private string leftRegisterName;
        private string right;

        public long RightValue
        {
            get
            {
                if (string.IsNullOrEmpty(right))
                    throw new ArgumentException("Value is not set");
                try
                {
                    return Convert.ToInt64(right);
                }
                catch (FormatException)
                {
                    return registers[right];
                }
            }
        }

        public long LeftValue
        {
            get => registers[leftRegisterName];
            set => registers[leftRegisterName] = value;
        }
        
        public Instruction(string[] strings)
        {
            Command = strings[0];
            leftRegisterName = strings[1];
            
            if (!registers.ContainsKey(leftRegisterName))
                registers.Add(leftRegisterName, 0);
            
            if (strings.Length > 2)
                right = strings[2];    
        }

        public override string ToString()
        {
            return $"{nameof(Command)}: {Command}, {nameof(leftRegisterName)}: {leftRegisterName}, {nameof(right)}: {right}";
        }
    }
}