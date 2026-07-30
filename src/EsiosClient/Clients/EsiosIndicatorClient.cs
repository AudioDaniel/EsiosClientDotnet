using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EsiosClient.Core;
using EsiosClient.Models;

namespace EsiosClient.Clients;

internal class EsiosIndicatorClient : EsiosClientBase, IEsiosIndicatorClient
{
    private const string IndicatorsEndpoint = "/indicators";

    public EsiosIndicatorClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<string> GetIndicatorsRawAsync(CancellationToken cancellationToken = default)
    {
        return await GetWithVersionAsync(IndicatorsEndpoint, "v1", cancellationToken);
    }

    public async Task<EsiosIndicatorResponse?> GetIndicatorsAsync(CancellationToken cancellationToken = default)
    {
        return await GetFromJsonAsync<EsiosIndicatorResponse>(IndicatorsEndpoint, "v1", cancellationToken);
    }
}
