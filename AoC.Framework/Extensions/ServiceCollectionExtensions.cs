using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AoC.Framework.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAoCFramework(this IServiceCollection services, Action<AoCOptions>? configureOptions = null)
    {
        services.AddLogging(l => l.AddConsole())
            .Configure(configureOptions ?? (o => { }))
            .AddSingleton<AoCCache>()
            .AddSingleton<AoC>();
        
        services.AddHttpClient<AoCHttpClient>(client =>
        {
            client.BaseAddress = new Uri("https://adventofcode.com/");
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(+https://github.com/lyze237/AoC.Framework)"));
        });

        return services;
    }
}