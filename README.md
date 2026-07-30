# EsiosClient

A reusable .NET client library for consuming the [ESIOS API](https://www.esios.ree.es/).

## Overview

`EsiosClient` provides a clean, strongly-typed interface (`IEsiosClient`) to interact with the ESIOS API. It abstracts away the HTTP communication and authentication headers, allowing you to easily integrate ESIOS data into any .NET application (ASP.NET Core, Worker Services, MAUI, Xamarin, etc.).

## Features

- **Dependency Injection Ready**: Seamlessly integrates with `Microsoft.Extensions.DependencyInjection`.
- **HttpClientFactory Integration**: Built on top of `AddHttpClient` to ensure proper connection pooling and lifetime management.
- **Easy Configuration**: Simple options pattern to configure the base address and personal access token.

## Installation

You can install the library via NuGet Package Manager or the .NET CLI.

**Using .NET CLI:**
```bash
dotnet add package EsiosClient
```

**Using Package Manager Console:**
```powershell
Install-Package EsiosClient
```

> **Note:** If this package is not yet published to NuGet.org, you can add a direct project reference to the `EsiosClient.csproj` or pack it locally using `dotnet pack` and consume the generated `.nupkg`.

## Usage

### 1. Register the Client

In your application startup (e.g., `Program.cs`), register the `EsiosClient` and configure your authentication token:

```csharp
using EsiosClient;

var builder = WebApplication.CreateBuilder(args);

// Add the ESIOS client to the service collection
builder.Services.AddEsiosClient(options =>
{
    // The BaseAddress defaults to "https://api.esios.ree.es/" but can be overridden if needed
    // options.BaseAddress = "https://api.esios.ree.es/";
    
    // Provide your ESIOS personal access token here
    options.PersonalToken = builder.Configuration["Esios:Token"] ?? "YOUR_TOKEN";
});
```

### 2. Inject and Use

Inject the `IEsiosClient` into your services or controllers to start making requests. The client returns fully strongly-typed C# models matching the JSON structures of the ESIOS API.

```csharp
using System.Threading;
using System.Threading.Tasks;
using EsiosClient;

public class MyElectricityService
{
    private readonly IEsiosClient _esiosClient;

    public MyElectricityService(IEsiosClient esiosClient)
    {
        _esiosClient = esiosClient;
    }

    public async Task FetchDataAsync(CancellationToken cancellationToken = default)
    {
        // 1. Check if the API is healthy
        bool isHealthy = await _esiosClient.CheckHealthAsync(cancellationToken);
        
        if (isHealthy)
        {
            // 2. Fetch indicators (Strongly Typed!)
            var indicatorResponse = await _esiosClient.Indicators.GetIndicatorsAsync(cancellationToken);
            
            if (indicatorResponse != null)
            {
                foreach (var ind in indicatorResponse.Indicators)
                {
                    Console.WriteLine($"{ind.Id}: {ind.Name}");
                }
            }
        }
    }
}
```

### 3. Using Query Parameters

Endpoints like `GetDocumentationsAsync` and `GetGlossariesAsync` support filtering by taxonomy terms and vocabularies using the `EsiosContentQuery` object.

```csharp
using EsiosClient.Models;

var query = new EsiosContentQuery
{
    TaxonomyTerms = new[] { "taxonomy_term_1" },
    Vocabularies = new[] { "vocabulary_1" }
};

// This generates a request to: /es/glossaries?taxonomy_terms[]=taxonomy_term_1&vocabularies[]=vocabulary_1
var glossariesResponse = await _esiosClient.Content.GetGlossariesAsync(query);
```

## Available Methods

The `IEsiosClient` interface acts as a facade, exposing specific domain clients. Every endpoint comes with both a strongly-typed parsing method (e.g., `GetIndicatorsAsync()`) and a Raw method that returns the JSON string (e.g., `GetIndicatorsRawAsync()`).

- `client.Indicators.GetIndicatorsAsync()`: Fetches the indicators data from `/indicators` (API v1).
- `client.Archives.GetArchivesAsync()`: Fetches the archives data from `/archives` (API v1).
- `client.Auctions.GetAuctionsAsync()`: Fetches the auctions data from `/auctions` (API v1).
- `client.Widgets.GetCachedWidgetAsync(int id)`: Fetches data for a specific cached widget from `/cached_widgets/{id}` (API v2).
- `client.Content.GetDocumentationsAsync(EsiosContentQuery query)`: Fetches documentations allowing filtering by taxonomy terms and vocabularies (API v1).
- `client.Content.GetGlossariesAsync(EsiosContentQuery query)`: Fetches glossaries allowing filtering by taxonomy terms and vocabularies (API v1).

Other top-level methods on `IEsiosClient`:
- `GetAsync(string endpoint)`: A generic method to perform a GET request against any other ESIOS endpoint using API v1.
- `CheckHealthAsync()`: Validates that the ESIOS API is reachable.
- `VerifyTokenAsync()`: Validates that your configured `PersonalToken` is authorized.
