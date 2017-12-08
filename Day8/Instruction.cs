using System;

namespace Day8
{
    public class Instruction
    {
        public string Name { get; set; }

        public string OnWho { get; set; }
        public int ChangeAmount { get; set; }
        public bool IsInc { get; set; }
        public string Condition { get; set; }
        public int Amount { get; set; }

        public Instruction(string name, string onWho, int changeAmount, bool isInc, string condition, int amount)
        {
            Name = name;
            OnWho = onWho;
            ChangeAmount = changeAmount;
            IsInc = isInc;
            Condition = condition;
            Amount = amount;
        }
    }
}