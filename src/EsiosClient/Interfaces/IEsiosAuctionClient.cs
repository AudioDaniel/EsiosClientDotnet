using System.Threading;
using System.Threading.Tasks;
using EsiosClient.Models;

namespace EsiosClient;

public interface IEsiosAuctionClient
{
    Task<string> GetAuctionsRawAsync(CancellationToken cancellationToken = default);
    Task<EsiosAuctionResponse?> GetAuctionsAsync(CancellationToken cancellationToken = default);
}
