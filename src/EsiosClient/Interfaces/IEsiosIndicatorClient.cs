using System.Threading;
using System.Threading.Tasks;
using EsiosClient.Models;

namespace EsiosClient;

public interface IEsiosIndicatorClient
{
    Task<string> GetIndicatorsRawAsync(CancellationToken cancellationToken = default);
    Task<EsiosIndicatorResponse?> GetIndicatorsAsync(CancellationToken cancellationToken = default);
}
