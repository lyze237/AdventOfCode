using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day23 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        var muls = 0;
        var instructions = Input.Select(line => line.Split(' ')).Select(strings => new Instruction(strings)).ToList();

        for (var i = 0; i < instructions.Count; i++)
        {
            var instruction = instructions[i];
                
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

    public override object ExecutePart2()
    {
        long h = 0;

        long b = Convert.ToInt32(Input[0].Replace("set b ", ""));
        b = b * 100 + 100000;
        var c = b + 17000;
            
        for (; b != c; b += 17)
        {
            for (var d = 2; d < b; d++)
            {
                if (b % d == 0)
                {
                    h++;
                    break;
                }
            }
        }
        return h + 1;
    }

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