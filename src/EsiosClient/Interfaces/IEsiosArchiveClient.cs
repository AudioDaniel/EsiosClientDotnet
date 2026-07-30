using System.Threading;
using System.Threading.Tasks;
using EsiosClient.Models;

namespace EsiosClient;

public interface IEsiosArchiveClient
{
    Task<string> GetArchivesRawAsync(CancellationToken cancellationToken = default);
    Task<EsiosArchiveResponse?> GetArchivesAsync(CancellationToken cancellationToken = default);
}
