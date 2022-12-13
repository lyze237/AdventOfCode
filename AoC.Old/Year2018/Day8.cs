using AdventOfCode.Parsers;
using Tidy.AdventOfCode;

namespace AdventOfCode.Year2018;

public class Day8 : Day<string[]>.WithParser<SpaceParser>
{
    public override object ExecutePart1()
    {
        var numbers = Input.Select(int.Parse).ToList();

        var i = 0;
        var node = Node.GetNode(numbers, ref i);

        return node.AllSum;
    }

    public override object ExecutePart2()
    {
        var numbers = Input.Select(int.Parse).ToList();

        var i = 0;
        var node = Node.GetNode(numbers, ref i);

        return node.Value;
    }

    private class Node
    {
        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<int> Metadata { get; set; } = new List<int>();

        public int AllSum => Metadata.Sum() + Nodes.Sum(x => x.AllSum);
        public int Sum => Metadata.Sum();
        
        public int Value {
            get 
            {
                if (!Nodes.Any()) 
                    return Sum;

                var value = 0;
                foreach (int metadata in Metadata) 
                {
                    if (metadata <= Nodes.Count) 
                    {
                        value += Nodes[metadata - 1].Value;
                    }
                }
                return value;
            }
        }

        public static Node GetNode(List<int> numbers, ref int i)
        {
            var node = new Node();

            int children = numbers[i++];
            int metadata = numbers[i++];

            for (var j = 0; j < children; j++)
            {
                node.Nodes.Add(GetNode(numbers, ref i));
            }

            for (var j = 0; j < metadata; j++)
            {
                node.Metadata.Add(numbers[i++]);
            }

            return node;
        }
    }
}