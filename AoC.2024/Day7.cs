using AoC.Framework;
using AoC.Framework.Extensions;

namespace AoC._2024;

public class Day7() : Day<(ulong result, ulong[] numbers)[]>(2024, 7)
{
    private record Operator(Func<ulong, ulong, ulong> Method, string Name)
    {
        public override string ToString() => Name;
    }

    protected override object DoPart1((ulong result, ulong[] numbers)[] input) =>
        CalculateCombinations(input, [
            new Operator((a, b) => a * b, "*"),
            new Operator((a, b) => a + b, "+")
        ]);

    protected override object DoPart2((ulong result, ulong[] numbers)[] input) =>
        CalculateCombinations(input, [
            new Operator(ConcatenateNumbers, "||"),
            new Operator((a, b) => a * b, "*"),
            new Operator((a, b) => a + b, "+")
        ]);

    private static ulong CalculateCombinations((ulong result, ulong[] numbers)[] input, List<Operator> operators)
    {
        ulong validEquations = 0;

        foreach (var (expectedResult, numbers) in input)
        {
            var allCombinations = GenerateCombinations(operators, numbers.Length - 1);

            if (IsValidCombination(numbers, expectedResult, allCombinations))
                validEquations += expectedResult;
        }

        return validEquations;
    }

    private static bool IsValidCombination(ulong[] numbers, ulong expectedResult, List<List<Operator>> allCombinations)
    {
        foreach (var combination in allCombinations)
        {
            var result = numbers[0];
            for (var index = 1; index < numbers.Length; index++)
                result = combination[index - 1].Method(result, numbers[index]);

            if (result != expectedResult)
                continue;

            return true;
        }

        return false;
    }

    private static List<List<Operator>> GenerateCombinations(List<Operator> operators, int length)
    {
        var results = new List<List<Operator>>();
        GenerateCombinationsRecursive(operators, length, [], results);
        return results;
    }

    private static void GenerateCombinationsRecursive(List<Operator> operators, int length, List<Operator> currentCombination, List<List<Operator>> results)
    {
        if (currentCombination.Count == length)
        {
            results.Add([..currentCombination]);
            return;
        }

        foreach (var op in operators)
        {
            currentCombination.Add(op);
            GenerateCombinationsRecursive(operators, length, currentCombination, results);
            currentCombination.RemoveAt(currentCombination.Count - 1);
        }
    }

    private static ulong ConcatenateNumbers(ulong num1, ulong num2)
    {
        ulong pow = 10;
        while (num2 >= pow)
            pow *= 10;
        
        return num1 * pow + num2;
    }
    
    protected override (ulong result, ulong[] numbers)[] ParseInput(string input) =>
        input
            .Split("\n")
            .Select(i => i.Split(":"))
            .Select(i => (i[0].Trim().ToULong(), i[1].Trim().Split(" ").Select(ulong.Parse).ToArray()))
            .ToArray();
}