using System.Threading;
using System.Threading.Tasks;
using EsiosClient.Models;

namespace EsiosClient;

public interface IEsiosWidgetClient
{
    Task<string> GetCachedWidgetRawAsync(int id, CancellationToken cancellationToken = default);
    Task<EsiosWidgetResponse?> GetCachedWidgetAsync(int id, CancellationToken cancellationToken = default);
}
