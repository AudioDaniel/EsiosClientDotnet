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

    public async Task<string> GetAuctionsRawAsync(CancellationToken cancellationToken = default)
    {
        return await GetWithVersionAsync(AuctionsEndpoint, "v1", cancellationToken);
    }

    public async Task<EsiosAuctionResponse?> GetAuctionsAsync(CancellationToken cancellationToken = default)
    {
        return await GetFromJsonAsync<EsiosAuctionResponse>(AuctionsEndpoint, "v1", cancellationToken);
    }
}
