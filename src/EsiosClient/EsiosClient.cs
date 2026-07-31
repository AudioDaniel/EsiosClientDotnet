using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EsiosClient.Clients;
using EsiosClient.Core;

namespace EsiosClient;

/// <summary>
/// Implementation of IEsiosClient for consuming the ESIOS API.
/// </summary>
public class EsiosClient : EsiosClientBase, IEsiosClient
{
    public IEsiosArchiveClient Archives { get; }
    public IEsiosIndicatorClient Indicators { get; }
    public IEsiosContentClient Content { get; }
    public IEsiosWidgetClient Widgets { get; }
    public IEsiosAuctionClient Auctions { get; }

    public EsiosClient(HttpClient httpClient) : base(httpClient)
    {
        Archives = new EsiosArchiveClient(httpClient);
        Indicators = new EsiosIndicatorClient(httpClient);
        Content = new EsiosContentClient(httpClient);
        Widgets = new EsiosWidgetClient(httpClient);
        Auctions = new EsiosAuctionClient(httpClient);
    }

    public async Task<string> GetAsync(string endpoint, CancellationToken cancellationToken = default)
    {
        return await GetWithVersionAsync(endpoint, "v1", cancellationToken);
    }

    public Task<bool> CheckHealthAsync(CancellationToken cancellationToken = default)
    {
        return TryHandleHttpRequestException(async () =>
        {
            using var request = new HttpRequestMessage(HttpMethod.Head, "/indicators");
            request.Headers.TryAddWithoutValidation("Accept", "application/json; application/vnd.esios-api-v1+json");
            
            var response = await HttpClient.SendAsync(request, cancellationToken);
            
            if (response.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed)
            {
                using var getRequest = new HttpRequestMessage(HttpMethod.Get, "/indicators");
                getRequest.Headers.TryAddWithoutValidation("Accept", "application/json; application/vnd.esios-api-v1+json");
                response = await HttpClient.SendAsync(getRequest, cancellationToken);
            }

            return (int)response.StatusCode >= 200 && (int)response.StatusCode < 500;
        });
    }

    public Task<bool> VerifyTokenAsync(CancellationToken cancellationToken = default)
    {
        return TryHandleHttpRequestException(async () =>
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "/indicators");
            request.Headers.TryAddWithoutValidation("Accept", "application/json; application/vnd.esios-api-v1+json");
            var response = await HttpClient.SendAsync(request, cancellationToken);
            
            // If the server explicitly responds with an authorization error, the token is invalid.
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || 
                response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return false;
            }

            return response.IsSuccessStatusCode;
        });
    }

    private async Task<bool> TryHandleHttpRequestException(Func<Task<bool>> action)
    {
        try
        {
            return await action();
        }
        catch (HttpRequestException)
        {
            return false;       
        }
    }
}