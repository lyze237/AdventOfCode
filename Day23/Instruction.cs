using System;
using System.Collections.Generic;

namespace Day18
{
    public class Instruction
    {
        public static Dictionary<int, Dictionary<string, long>> Registers = new Dictionary<int, Dictionary<string, long>>();
        
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
                    return Registers[programId][right];
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
                    return Registers[programId][left];
                }
            }
            set => Registers[programId][left] = value;
        }

        public Instruction(string[] strings, int programId = 0)
        {
            this.programId = programId;
            Command = strings[0];
            left = strings[1];

            if (!Registers.ContainsKey(programId))
                Registers.Add(programId, new Dictionary<string, long>());

            try
            {
                Convert.ToInt64(left);
            }
            catch (FormatException)
            {
                if (!Registers[programId].ContainsKey(left))
                {
                    Registers[programId].Add(left, left == "a" ? programId : 0);
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