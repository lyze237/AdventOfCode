using System.Text.Json.Nodes;
using AoC.Framework;
using AoC.Framework.Extensions;
using NUnit.Framework;

namespace AoC._2022;

[TestFixture]
public class Day13 : Day<string>
{
    public Day13() : base(2022, 13) { }

    protected override object DoPart1(string input)
    {
        var pairs = new List<(JsonNode left, JsonNode right)>();

        foreach (var stringPairs in input.Split("\n\n"))
        {
            var (leftString, rightString, _) = stringPairs.Split("\n");
            pairs.Add((JsonNode.Parse(leftString!)!, JsonNode.Parse(rightString!))!);
        }

        return pairs.Select((pair, i) => CompareNodes(pair.left, pair.right) == true ? i + 1 : 0).Sum();
        
    }

    protected override object DoPart2(string input)
    {
        var nodes = input.Split("\n").Where(i => !string.IsNullOrEmpty(i)).Select(i => JsonNode.Parse(i)!).ToList();

        var (first, second) = (JsonNode.Parse("[[2]]")!, JsonNode.Parse("[[6]]")!);
        nodes.AddRange(new[] { first, second });

        nodes.Sort((left, right) => CompareNodes(left, right) == true ? -1 : 1);

        return (nodes.IndexOf(first) + 1) * (nodes.IndexOf(second) + 1);
    }
    
    private static bool? CompareNodes(JsonNode leftNode, JsonNode rightNode)
    {
        if (leftNode is JsonValue leftValue && rightNode is JsonValue rightValue)
            return leftValue.GetValue<int>() == rightValue.GetValue<int>() ? null : leftValue.GetValue<int>() < rightValue.GetValue<int>();

        if (leftNode is not JsonArray leftArray)
            leftArray = new JsonArray(leftNode.GetValue<int>());

        if (rightNode is not JsonArray rightArray)
            rightArray = new JsonArray(rightNode.GetValue<int>());

        for (var i = 0; i < leftArray.Count && i < rightArray.Count; i++)
        {
            var result = CompareNodes(leftArray[i]!, rightArray[i]!);

            if (result.HasValue)
                return result.Value;
        }

        return (leftArray.Count - rightArray.Count) switch
        {
            < 0 => true,
            > 0 => false,
            _ => null,
        };
    }

    protected override string ParseInput(string input) =>
        input;
}