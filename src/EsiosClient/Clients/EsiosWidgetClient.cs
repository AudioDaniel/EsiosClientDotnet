using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EsiosClient.Core;
using EsiosClient.Models;

namespace EsiosClient.Clients;

internal class EsiosWidgetClient : EsiosClientBase, IEsiosWidgetClient
{
    private const string CachedWidgetsEndpoint = "/cached_widgets";

    public EsiosWidgetClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<string> GetCachedWidgetRawAsync(int id, CancellationToken cancellationToken = default)
    {
        return await GetWithVersionAsync($"{CachedWidgetsEndpoint}/{id}", "v2", cancellationToken);
    }

    public async Task<EsiosWidgetResponse?> GetCachedWidgetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await GetFromJsonAsync<EsiosWidgetResponse>($"{CachedWidgetsEndpoint}/{id}", "v2", cancellationToken);
    }
}
