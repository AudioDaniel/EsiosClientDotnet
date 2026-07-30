using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EsiosClient.Core;

public abstract class EsiosClientBase
{
    protected readonly HttpClient HttpClient;

    protected EsiosClientBase(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    protected async Task<string> GetWithVersionAsync(string endpoint, string version, CancellationToken cancellationToken = default)
    {
        if (!endpoint.StartsWith("/"))
        {
            endpoint = $"/{endpoint}";
        }

        using var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        request.Headers.TryAddWithoutValidation("Accept", $"application/json; application/vnd.esios-api-{version}+json");
        
        var response = await HttpClient.SendAsync(request, cancellationToken);
        
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    protected async Task<T?> GetFromJsonAsync<T>(string endpoint, string version, CancellationToken cancellationToken = default)
    {
        var json = await GetWithVersionAsync(endpoint, version, cancellationToken);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<T>(json, options);
    }
}
