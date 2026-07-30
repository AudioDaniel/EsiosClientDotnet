using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EsiosClient.Core;
using EsiosClient.Models;

namespace EsiosClient.Clients;

internal class EsiosAuctionClient : EsiosClientBase, IEsiosAuctionClient
{
    private const string AuctionsEndpoint = "/auctions";

    public EsiosAuctionClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<string> GetAuctionsRawAsync(DateTime? year = null, CancellationToken cancellationToken = default)
    {
        string endpoint = AuctionsEndpoint;
        if (year.HasValue)
        {
            // The ESIOS API often fails to parse full datetime strings and expects either 'date=yyyy-MM-dd' or 'year=yyyy'
            endpoint += $"?date={year.Value:yyyy-MM-dd}&year={year.Value.Year}";
        }
        return await GetWithVersionAsync(endpoint, "v1", cancellationToken);
    }

    public async Task<EsiosAuctionResponse?> GetAuctionsAsync(DateTime? year = null, CancellationToken cancellationToken = default)
    {
        string endpoint = AuctionsEndpoint;
        if (year.HasValue)
        {
            endpoint += $"?date={year.Value:yyyy-MM-dd}&year={year.Value.Year}";
        }
        return await GetFromJsonAsync<EsiosAuctionResponse>(endpoint, "v1", cancellationToken);
    }
}
