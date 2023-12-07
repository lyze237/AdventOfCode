using AoC.Framework.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace AoC.Framework;

public abstract class Day : Day<string[]>
{
    protected override string[] ParseInput(string input) => 
        input.Split("\n");

    protected Day(int year, int day, bool skipExamples = false) : base(year, day, skipExamples)
    {
    }
}

[TestFixture]
public abstract class Day<T>
{
    private readonly int year;
    private readonly int day;
    private readonly bool skipExamples;

    private readonly AoC aoc;
    private readonly ILogger<AoC> logger;

    protected Day(int year, int day, bool skipExamples = false)
    {
        this.year = year;
        this.day = day;
        this.skipExamples = skipExamples;

        var serviceProvider = new ServiceCollection().AddAoCFramework().BuildServiceProvider();
        logger = serviceProvider.GetService<ILogger<AoC>>()!;
        aoc = serviceProvider.GetRequiredService<AoC>();
    }

    [OneTimeSetUp]
    public async Task Setup() => 
        await aoc.FetchDay(year, day);

    [Test, Order(1)]
    public async Task Part1()
    {
        // Example
        logger.LogInformation("{Year}-{Day}-{Part}: Running Example", year, day, 1);
        var input = aoc.FindExampleInput()!;
        if (!skipExamples)
            Console.WriteLine("\n\n\nExample 1:\n");
        
        var userAnswer = DoPart1(ParseInput(input));
        var exampleAnswer = aoc.FindExampleAnswer(1);

        if (!skipExamples)
        {
            logger.LogInformation("{Year}-{Day}-{Part}: Example answer found on website, checking that instead: '{Answer}' == '{User}'", year, day, 1, exampleAnswer, userAnswer);
            Assert.AreEqual(exampleAnswer, userAnswer?.ToString(), "Example Part 1 Failed");
        }
        
        logger.LogInformation("{Year}-{Day}-{Part}: Running Actual Part", year, day, 1);
        // Actual
        Console.WriteLine("\n\n\nActual 1:\n");
        userAnswer = DoPart1(ParseInput(aoc.Input));

        var answerOnPage = aoc.FindAnswer(1);
        if (answerOnPage == null)
        {
            logger.LogInformation("{Year}-{Day}-{Part}: Answer not found on website, submitting {Answer}", year, day, 1, userAnswer);
            var (status, error) = await aoc.SubmitInput(year, day, 1, userAnswer?.ToString() ?? "null");
            switch (status)
            {
                case null:
                    logger.LogWarning("{Year}-{Day}-{Part}: Api didn't like us: {Error}", year, day, 1, error);
                    Assert.Inconclusive(error);
                    return;
                case true when aoc.FindExampleAnswer(2) == null:
                    await aoc.FetchDay(year, day, true);
                    break;
            }

            logger.LogInformation("{Year}-{Day}-{Part}: Did we win?: {Status}", year, day, 1, status);
            Assert.IsTrue(status, error);
            return;
        }
        
        logger.LogInformation("{Year}-{Day}-{Part}: Actual answer found on website, checking that instead: '{Answer}' == '{User}'", year, day, 1, answerOnPage, userAnswer);
        Assert.AreEqual(answerOnPage, userAnswer?.ToString(), "Actual Part 1 Failed");
    }
    
    [Test, Order(2)]
    public async Task Part2()
    {
        // Example
        logger.LogInformation("{Year}-{Day}-{Part}: Running Example", year, day, 2);
        var input = aoc.FindExampleInput()!;
        if (!skipExamples)
            Console.WriteLine("\n\n\nExample 2:\n");
        var userAnswer = DoPart2(ParseInput2(input));
        var exampleAnswer = aoc.FindExampleAnswer(2);

        if (userAnswer == null)
            Assert.Ignore("Part 2 isn't implemented yet or returns null");

        if (exampleAnswer == null)
            Assert.Ignore("Part 2 hasn't been discovered yet");

        if (!skipExamples)
        {
            logger.LogInformation("{Year}-{Day}-{Part}: Example answer found on website, checking that instead: '{Answer}' == '{User}'", year, day, 2, exampleAnswer, userAnswer);
            Assert.AreEqual(exampleAnswer, userAnswer.ToString(), "Example Part 2 Failed");
        }
        
        
        logger.LogInformation("{Year}-{Day}-{Part}: Running Actual", year, day, 2);
        // Actual
        Console.WriteLine("\n\n\nActual 2:\n");
        userAnswer = DoPart2(ParseInput2(aoc.Input));

        var answerOnPage = aoc.FindAnswer(2);
        if (answerOnPage == null)
        {
            logger.LogInformation("{Year}-{Day}-{Part}: Answer not found on website, submitting {Answer}", year, day, 2, userAnswer);
            var (status, error) = await aoc.SubmitInput(year, day, 2, userAnswer?.ToString() ?? "null");
            if (status == null)
            {
                logger.LogWarning("{Year}-{Day}-{Part}: Api didn't like us: {Error}", year, day, 2, error);
                Assert.Inconclusive(error);
                return;
            }

            logger.LogInformation("{Year}-{Day}-{Part}: Did we win?: {Status}", year, day, 2, status);
            if (status == true)
                await aoc.FetchDay(year, day, true);
            
            Assert.IsTrue(status, error);

            return;
        }
        
        logger.LogInformation("{Year}-{Day}-{Part}: Actual answer found on website, checking that instead: '{Answer}' == '{User}'", year, day, 2, answerOnPage, userAnswer);
        Assert.AreEqual(answerOnPage, userAnswer?.ToString(), "Actual Part 2 Failed");
    }
    
    protected abstract object? DoPart1(T input);
    protected abstract object? DoPart2(T input);

    protected abstract T ParseInput(string input);

    protected virtual T ParseInput2(string input) => 
        ParseInput(input);
}