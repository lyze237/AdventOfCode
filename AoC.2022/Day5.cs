﻿using System.Text.RegularExpressions;
using AoC.Framework;
using NUnit.Framework;

namespace AoC._2022;

[TestFixture]
public class Day5 : Day
{
    public Day5() : base(2022, 5) { }

    protected override object DoPart1(string[] input)
    {
        var (stacks, moves) = (ParseStacks(input), ParseMoves(input));
        
        foreach (var (move, from, to) in moves)
        {
            for (var i = 0; i < move; i++)
            {
                var popped = stacks[from - 1].Pop();
                stacks[to - 1].Push(popped);
            }
        }

        return stacks.Aggregate("", (current, stack) => current + stack.Pop());
    }

    protected override object DoPart2(string[] input)
    {
        var (stacks, moves) = (ParseStacks(input), ParseMoves(input));

        var cratesPickedUp = new List<char>();
        foreach (var (move, from, to) in moves)
        {
            cratesPickedUp.Clear();
            
            for (var i = 0; i < move; i++)
                cratesPickedUp.Add(stacks[from - 1].Pop());

            for (var index = cratesPickedUp.Count - 1; index >= 0; index--)
                stacks[to - 1].Push(cratesPickedUp[index]);
        }

        return stacks.Aggregate("", (current, stack) => current + stack.Pop());
    }
    
    private List<(int move, int from, int to)> ParseMoves(string[] input)
    {
        // Find where moves lines start
        var movesStartLine = 0;
        for (; !input[movesStartLine].Trim().StartsWith("m"); movesStartLine++) { }
        
        // Just do a basic regex match, I'm lazy
        var regex = new Regex(@"move (?<move>\d+) from (?<from>\d+) to (?<to>\d+)");

        // Who needs classes
        var moves = new List<(int move, int from, int to)>();
        for (var i = movesStartLine; i < input.Length; i++)
        {
            var match = regex.Match(input[i]);
            
            moves.Add((Convert.ToInt32(match.Groups["move"].Value), Convert.ToInt32(match.Groups["from"].Value), Convert.ToInt32(match.Groups["to"].Value)));
        }

        return moves;
    }

    private List<Stack<char>> ParseStacks(string[] input)
    {
        // Find where stack count line is
        var stackCountLine = 0;
        for (; !input[stackCountLine].Trim().StartsWith("1"); stackCountLine++) { }

        var stackInputLine = input[stackCountLine];

        // Find amount of stacks
        var stackAmounts = Convert.ToInt32(stackInputLine.Trim().Last().ToString());

        // Initialize stack array
        var stacks = new List<Stack<char>>();
        Enumerable.Range(0, stackAmounts).ToList().ForEach(_ => stacks.Add(new Stack<char>()));

        for (var reverseIndex = stackCountLine - 1; reverseIndex >= 0; reverseIndex--)
        {
            // Every line iterate through the stack line and find out where the indexes are
            for (var stackLineIndex = 0; stackLineIndex < stackInputLine.Length; stackLineIndex++)
            {
                if (!char.IsDigit(stackInputLine[stackLineIndex]))
                    continue;

                // If we found an index, convert that to a number
                var currentStack = Convert.ToInt32(stackInputLine[stackLineIndex].ToString()) - 1;

                // If that crate on the line we look at actually exists, add it to the stack
                var crateInput = input[reverseIndex][stackLineIndex];
                if (crateInput != ' ')
                    stacks[currentStack].Push(crateInput);
            }
        }

        return stacks;
    }
}