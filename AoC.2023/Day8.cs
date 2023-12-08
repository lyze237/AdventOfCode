using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2023;

[TestFixture]
public class Day8 : Day<(char[] directions, Day8.Node[] nodes)>
{
    public record Node(string Name, string Left, string Right);

    public Day8() : base(2023, 8, true)
    {
    }

    protected override object DoPart1((char[] directions, Node[] nodes) input) => 
        FindZNode("AAA", input, true);

    protected override object DoPart2((char[] directions, Node[] nodes) input) =>
        input.nodes
            .Where(n => n.Name.Contains('A'))
            .AsParallel()
            .Select(c => FindZNode(c.Name, input, false))
            .FindLcm();

    private static int FindZNode(string startNode, (char[] directions, Node[] nodes) input, bool allThree)
    {
        var steps = 0;
        
        var currentNode = startNode;
        foreach (var direction in input.directions.RepeatIndefinitely())
        {
            var node = input.nodes.First(n => n.Name == currentNode);
            currentNode = direction switch
            {
                'L' => node.Left,
                'R' => node.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction))
            };

            steps++;

            if (allThree ? currentNode == "ZZZ" : currentNode.Contains('Z'))
                return steps;
        }

        return 0;
    }

    protected override (char[] directions, Node[] nodes) ParseInput(string input)
    {
        var lines = input.Split("\n");
        var directions = lines[0].ToCharArray();

        var nodes = lines
            .Skip(2)
            .Select(line => Regex.Match(line, @"(?<name>\w+) = \((?<left>\w+), (?<right>\w+)\)"))
            .Select(match => new Node(match.Groups["name"].Value, match.Groups["left"].Value, match.Groups["right"].Value))
            .ToArray();

        return (directions, nodes);
    }
}