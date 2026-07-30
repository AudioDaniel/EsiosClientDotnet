using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EsiosClient.Core;
using EsiosClient.Models;

namespace EsiosClient.Clients;

internal class EsiosArchiveClient : EsiosClientBase, IEsiosArchiveClient
{
    private const string ArchivesEndpoint = "/archives";

    public EsiosArchiveClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<string> GetArchivesRawAsync(CancellationToken cancellationToken = default)
    {
        return await GetWithVersionAsync(ArchivesEndpoint, "v1", cancellationToken);
    }

    public async Task<EsiosArchiveResponse?> GetArchivesAsync(CancellationToken cancellationToken = default)
    {
        return await GetFromJsonAsync<EsiosArchiveResponse>(ArchivesEndpoint, "v1", cancellationToken);
    }
}
