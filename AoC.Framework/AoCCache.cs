using Microsoft.Extensions.Options;

namespace AoC.Framework;

public class AoCCache
{
    private readonly AoCOptions options;

    private readonly DirectoryInfo directory;

    public string Cookie => File.ReadAllText(directory.GetFiles("cookie.txt").SingleOrDefault()?.FullName ?? throw new InvalidOperationException($"Create a file at {Path.Combine(directory.FullName, "cookie.txt")} with the session cookie from https://adventofcode.com"));

    public AoCCache(IOptions<AoCOptions> options)
    {
        this.options = options.Value;

        directory = new DirectoryInfo(this.options.CacheDirectory);
        if (!directory.Exists)
            directory.Create();
    }

    public string? ReadInputFile(int year, int day)
    {
        var inputFile = GetFile("Input", year, day);
        return !inputFile.Exists ? null : File.ReadAllText(inputFile.FullName);
    }
    
    public string? ReadPageFile(int year, int day)
    {
        var inputFile = GetFile("Page", year, day);
        return !inputFile.Exists ? null : File.ReadAllText(inputFile.FullName);
    }
    
    public string? ReadAnswerFile(int year, int day, int part, string hash)
    {
        var inputFile = GetFile("Answer", year, day, part.ToString(), hash);
        return !inputFile.Exists ? null : File.ReadAllText(inputFile.FullName);
    }

    public void WriteInputFile(int year, int day, string text) => 
        File.WriteAllText(GetFile("Input", year, day).FullName, text);

    public void WritePageFile(int year, int day, string text) => 
        File.WriteAllText(GetFile("Page", year, day).FullName, text);
    
    public void WriteAnswerFile(int year, int day, int part, string hash, string text) => 
        File.WriteAllText(GetFile("Answer", year, day, part.ToString(), hash).FullName, text);
    
    private FileInfo GetFile(string type, int year, int day, params string[] otherParts)
    {
        var file = new FileInfo(Path.Combine(directory.FullName, type, year.ToString(), $"{day}{string.Join("-", otherParts)}.txt"));
        Directory.CreateDirectory(file.Directory!.FullName);
        return file;
    }
}