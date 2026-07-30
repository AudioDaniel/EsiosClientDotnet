using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EsiosClient.Models;

namespace EsiosClient;

public interface IEsiosContentClient
{
    Task<string> GetGlossariesRawAsync(EsiosContentQuery? query = null, CancellationToken cancellationToken = default);
    Task<EsiosContentResponse?> GetGlossariesAsync(EsiosContentQuery? query = null, CancellationToken cancellationToken = default);
    
    Task<string> GetDocumentationsRawAsync(EsiosContentQuery? query = null, CancellationToken cancellationToken = default);
    Task<EsiosContentResponse?> GetDocumentationsAsync(EsiosContentQuery? query = null, CancellationToken cancellationToken = default);
}
