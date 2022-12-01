using System.Collections.Concurrent;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2017;

public class Day18 : Day.NewLineSplitParsed<string>
{
    public override object ExecutePart1()
    {
        long frequency = 0;

        var instructions = Input.Select(line => line.Split(' ')).Select(strings => new Instruction(strings)).ToList();

        for (var i = 0;; i++)
        {
            var instruction = instructions[i];

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
                        i += (int)instruction.RightValue - 1;

                        if (i < 0 || i >= instructions.Count)
                            return -1;
                    }

                    break;
            }
        }
    }

    private bool programALocked;
    private bool programBLocked;

    private int programBSend;

    public override object ExecutePart2()
    {
        var queueA = new ConcurrentQueue<long>();
        var queueB = new ConcurrentQueue<long>();

        var a = new Thread(() => Function(0, queueA, queueB));
        var b = new Thread(() => Function(1, queueB, queueA));

        a.Start();
        b.Start();

        a.Join();
        b.Join();

        return programBSend;
    }

    private void Function(int programId, ConcurrentQueue<long> ourQueue, ConcurrentQueue<long> otherQueue)
    {
        var instructions = Input.Select(line => line.Split(' ')).Select(strings => new Instruction(strings, programId))
            .ToList();

        for (var i = 0;; i++)
        {
            var instruction = instructions[i];

            switch (instruction.Command)
            {
                case "snd":
                    Console.WriteLine($"program {programId} sends {instruction.LeftValue} / {ourQueue.Count}");
                    otherQueue.Enqueue(instruction.LeftValue);
                    if (programId == 1)
                        programBSend++;
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
                    long value;
                    while (!ourQueue.TryDequeue(out value))
                    {
                        if (programId == 0)
                            programALocked = true;
                        else
                            programBLocked = true;

                        if (programALocked && programBLocked)
                        {
                            Console.WriteLine("Both programs sleeping, detected a deadlock -> terminating");
                            return;
                        }
                    }

                    if (programId == 0)
                        programALocked = false;
                    else
                        programBLocked = false;

                    Console.WriteLine($"program {programId} receives {value} / {ourQueue.Count}");
                    instruction.LeftValue = value;
                    break;
                case "jgz":
                    if (instruction.LeftValue > 0)
                    {
                        i += (int)instruction.RightValue - 1;

                        if (i < 0 || i >= instructions.Count)
                            return;
                    }

                    break;
            }
        }
    }

    public class Instruction
    {
        private static Dictionary<int, Dictionary<string, long>> registers = new();

        public string Command { get; }
        private string? left;
        private string? right;

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

            if (!registers[programId].ContainsKey(left))
            {
                registers[programId].Add(left, left == "p" ? programId : 0);
            }

            if (strings.Length > 2)
                right = strings[2];
        }

        public override string ToString() => $"{nameof(Command)}: {Command}, {nameof(programId)}: {programId}, {nameof(left)}: {left}, {nameof(right)}: {right}";
    }
}