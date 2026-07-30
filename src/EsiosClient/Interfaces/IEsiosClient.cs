using System.Threading;
using System.Threading.Tasks;

namespace EsiosClient;

/// <summary>
/// The main entry point for the ESIOS API.
/// </summary>
public interface IEsiosClient
{
    IEsiosArchiveClient Archives { get; }
    IEsiosIndicatorClient Indicators { get; }
    IEsiosContentClient Content { get; }
    IEsiosWidgetClient Widgets { get; }
    IEsiosAuctionClient Auctions { get; }

    /// <summary>
    /// Performs a GET request to a custom endpoint.
    /// </summary>
    Task<string> GetAsync(string endpoint, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if the ESIOS API is reachable.
    /// </summary>
    Task<bool> CheckHealthAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates that the configured PersonalToken is authorized.
    /// </summary>
    Task<bool> VerifyTokenAsync(CancellationToken cancellationToken = default);
}
