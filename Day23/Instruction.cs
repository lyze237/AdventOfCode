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
