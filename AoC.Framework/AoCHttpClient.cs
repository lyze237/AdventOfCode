namespace AoC.Framework;

public class AoCHttpClient
{
    private readonly HttpClient client;
    private readonly AoCCache cache;

    public AoCHttpClient(HttpClient client, AoCCache cache)
    {
        this.client = client;
        this.cache = cache;
    }

    public async Task<string> DownloadInput(int year, int day)
    {
        client.DefaultRequestHeaders.Add("Cookie", $"session={cache.Cookie}");
        
        var response = await client.GetAsync($"/{year}/day/{day}/input");
        var content = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
        return content;
    }

    public async Task<string> DownloadPage(int year, int day)
    {
        client.DefaultRequestHeaders.Add("Cookie", $"session={cache.Cookie}");
        
        var response = await client.GetAsync($"/{year}/day/{day}");
        var content = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
        return content;
    }

    public async Task<string> SubmitAnswer(int year, int day, int part, string answer)
    {
        client.DefaultRequestHeaders.Add("Cookie", $"session={cache.Cookie}");

        var response = await client.PostAsync($"/{year}/day/{day}/answer", new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["level"] = $"{part}",
            ["answer"] = answer 
        }));

        var content = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
        return content;
    }
}