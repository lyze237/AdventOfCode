using System;
using System.Collections.Generic;

namespace Day18
{
    public class Instruction
    {
        private static Dictionary<int, Dictionary<string, long>> registers = new Dictionary<int, Dictionary<string, long>>();
        
        public string Command { get; }
        private string left;
        private string right;

        private int programId;

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
                    return registers[programId][right];
                }
            }
        }

        public long LeftValue
        {
            get
            {
                if (string.IsNullOrEmpty(left))
                    throw new ArgumentException("Value is not set");
                try
                {
                    return Convert.ToInt64(left);
                }
                catch (FormatException)
                {
                    return registers[programId][left];
                }
            }
            set => registers[programId][left] = value;
        }

        public Instruction(string[] strings, int programId = 0)
        {
            this.programId = programId;
            Command = strings[0];
            left = strings[1];

            if (!registers.ContainsKey(programId))
                registers.Add(programId, new Dictionary<string, long>());

            try
            {
                Convert.ToInt64(left);
            }
            catch (FormatException)
            {
                if (!registers[programId].ContainsKey(left))
                {
                    registers[programId].Add(left, left == "p" ? programId : 0);
                }
            }

            if (strings.Length > 2)
                right = strings[2];    
        }

        public override string ToString()
        {
            return $"{nameof(Command)}: {Command}, {nameof(programId)}: {programId}, {nameof(left)}: {left}, {nameof(right)}: {right}";
        }
    }
}