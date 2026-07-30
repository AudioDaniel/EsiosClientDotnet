using System;
using System.Threading;
using System.Threading.Tasks;
using EsiosClient.Models;

namespace EsiosClient;

public interface IEsiosAuctionClient
{
    Task<string> GetAuctionsRawAsync(DateTime? year = null, CancellationToken cancellationToken = default);
    Task<EsiosAuctionResponse?> GetAuctionsAsync(DateTime? year = null, CancellationToken cancellationToken = default);
}
