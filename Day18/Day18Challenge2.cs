using System.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Utils;

namespace Day18
{
    public class Day18Challenge2 : Challenge<long>
    {
        private bool programALocked;
        private bool programBLocked;
        
        private int programBSend;
        
        public Day18Challenge2() : base("input")
        {
        }

        public override long Run()
        {
            ConcurrentQueue<long> queueA = new ConcurrentQueue<long>();
            ConcurrentQueue<long> queueB = new ConcurrentQueue<long>();
            
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
            List<Instruction> instructions = new List<Instruction>();

            foreach (var line in GetInputFilePerLine())
            {
                var strings = line.Split(' ');
                instructions.Add(new Instruction(strings, programId));
            }
            
            for (int i = 0;; i++)
            {
                Instruction instruction = instructions[i];
                
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
                            i += (int) instruction.RightValue - 1;

                            if (i < 0 || i >= instructions.Count)
                                return;
                        }
                        break;
                }
            }
        }
    }
}