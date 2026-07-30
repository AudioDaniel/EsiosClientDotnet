using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using EsiosClient;
using EsiosClient.Models;

namespace EsiosClient.ConsoleTest;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        
        services.AddEsiosClient(options =>
        {
            options.PersonalToken = "request_your_personal_token_sending_email_to_consultasios@ree.es";
        });

        var serviceProvider = services.BuildServiceProvider();
        var esiosClient = serviceProvider.GetRequiredService<IEsiosClient>();

        Console.WriteLine("--- ESIOS API TEST ---\n");

        Console.WriteLine("1. Testing VerifyTokenAsync...");
        bool isValid = await esiosClient.VerifyTokenAsync();
        Console.WriteLine($"Token Is Valid: {isValid}\n");

        await TryEndpoint("Archives Raw", () => esiosClient.Archives.GetArchivesRawAsync());
        
        Console.WriteLine("Testing strongly-typed Archives...");
        // await TryEndpoint("Auctions", () => esiosClient.Auctions.GetAuctionsAsync());
        
        await TryEndpoint("Indicators", () => esiosClient.Indicators.GetIndicatorsRawAsync());
        var indObj = await esiosClient.Indicators.GetIndicatorsAsync();
        Console.WriteLine($"SUCCESS! Parsed {indObj?.Indicators.Count} indicators into C# objects.\n");

        await TryEndpoint("CachedWidget (1)", () => esiosClient.Widgets.GetCachedWidgetRawAsync(1));
        var widgetObj = await esiosClient.Widgets.GetCachedWidgetAsync(1);
        Console.WriteLine($"SUCCESS! Parsed Widget ID: {widgetObj?.Widget?.IdWidget} into C# object.\n");

        var contentQuery = new EsiosContentQuery 
        { 
            TaxonomyTerms = new[] { "term1" }, 
            Vocabularies = new[] { "vocab1" } 
        };
        await TryEndpoint("Documentations", () => esiosClient.Content.GetDocumentationsRawAsync(contentQuery));
        
        await TryEndpoint("Glossaries", () => esiosClient.Content.GetGlossariesRawAsync());
        var glosObj = await esiosClient.Content.GetGlossariesAsync();
        Console.WriteLine($"SUCCESS! Parsed {glosObj?.Contents.Count} glossaries into C# objects.\n");
    }

    static async Task TryEndpoint(string name, Func<Task<string>> action)
    {
        Console.WriteLine($"Testing {name}...");
        try
        {
            var result = await action();
            Console.WriteLine($"SUCCESS! Fetched {result.Length} chars of data.\n");
            System.IO.File.WriteAllText($"{name}.json", result);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP FAILED: {ex.StatusCode} - {ex.Message}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: {ex.Message}\n");
        }
    }
}
