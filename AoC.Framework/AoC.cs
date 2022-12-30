using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AoC.Framework;

public class AoC
{
    private AoCCache cache;
    private readonly AoCOptions options;
    private readonly AoCHttpClient client;
    private readonly ILogger<AoC> logger;

    public string Input { get; private set; } = null!;
    public string Page { get; private set; } = null!;

    private static MD5 md5 = MD5.Create();

    public AoC(AoCCache cache, IOptions<AoCOptions> options, AoCHttpClient client, ILogger<AoC> logger)
    {
        this.cache = cache;
        this.options = options.Value;
        this.client = client;
        this.logger = logger;
    }

    public async Task FetchDay(int year, int day, bool forceUpdate = false)
    {
        await FetchInput(year, day);
        await FetchPage(year, day, forceUpdate);
    }
    
    public async Task<(bool? status, string? error)> SubmitInput(int year, int day, int part, string text)
    {
        logger.LogInformation("{Year}-{Day}-{Part} Submitting: {Text}", year, day, part, text);
        
        var hash = text.Length <= 10 ? text : Encoding.UTF8.GetString(md5.ComputeHash(Encoding.UTF8.GetBytes(text)));

        var answerFile = cache.ReadAnswerFile(year, day, part, hash) ?? (await client.SubmitAnswer(year, day, part, text)).TrimEnd();

        var doc = new HtmlDocument();
        doc.LoadHtml(answerFile);
        var answerText = doc.DocumentNode.SelectSingleNode("//article").InnerText;
        logger.LogError("{Year}-{Day}-{Part} Answer: {Answer}", year, day, part, answerText);

        if (answerFile.Contains("You gave an answer too recently") || answerFile.Contains("You don't seem to be solving the right level"))
            return (null, answerText);

        cache.WriteAnswerFile(year, day, part, hash, answerFile);

        return (!answerFile.Contains("That's not the right answer"), answerText);
    }

    private async Task FetchInput(int year, int day)
    {
        var input = cache.ReadInputFile(year, day);
        
        if (input == null)
        {
            input = (await client.DownloadInput(year, day)).TrimEnd();
            cache.WriteInputFile(year, day, input);
        }

        Input = input;
    }

    private async Task FetchPage(int year, int day, bool forceUpdate = false)
    {
        var page = forceUpdate ? null : cache.ReadPageFile(year, day);

        if (page == null)
        {
            page = (await client.DownloadPage(year, day)).TrimEnd();
            cache.WritePageFile(year, day, page);
        }

        Page = page;
    }

    public string? FindExampleInput()
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(Page);

        var exampleNode = doc.DocumentNode.SelectSingleNode("//article[@class='day-desc']/p[(contains(., 'For') or contains(., 'for')) and (contains(., 'example') or contains(., 'Example')) and contains(., ':')]/following-sibling::pre/code");
        return HttpUtility.HtmlDecode(exampleNode.InnerText.TrimEnd());
    }
    
    public string? FindExampleAnswer(int part)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(Page);
        
        var solutionNode = doc.DocumentNode.SelectSingleNode($"//article[@class='day-desc'][{part}]/descendant::em[last() -1]");
        return HttpUtility.HtmlDecode(solutionNode?.InnerText);
    }
    
    public string? FindAnswer(int part)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(Page);
        
        var solutionNode = doc.DocumentNode.SelectSingleNode($"//article[@class='day-desc'][{part}]/following-sibling::p/code");
        return solutionNode?.InnerText;
    }
}