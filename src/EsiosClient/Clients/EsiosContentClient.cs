using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EsiosClient.Core;
using EsiosClient.Models;

namespace EsiosClient.Clients;

internal class EsiosContentClient : EsiosClientBase, IEsiosContentClient
{
    private const string GlossariesEndpoint = "/glossaries";
    private const string DocumentationsEndpoint = "/documentations";

    public EsiosContentClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<string> GetGlossariesRawAsync(EsiosContentQuery? query = null, CancellationToken cancellationToken = default)
    {
        string url = BuildTaxonomyQueryUrl("es", GlossariesEndpoint, query);
        return await GetWithVersionAsync(url, "v1", cancellationToken);
    }

    public async Task<EsiosContentResponse?> GetGlossariesAsync(EsiosContentQuery? query = null, CancellationToken cancellationToken = default)
    {
        string url = BuildTaxonomyQueryUrl("es", GlossariesEndpoint, query);
        return await GetFromJsonAsync<EsiosContentResponse>(url, "v1", cancellationToken);
    }

    public async Task<string> GetDocumentationsRawAsync(EsiosContentQuery? query = null, CancellationToken cancellationToken = default)
    {
        string url = BuildTaxonomyQueryUrl("es", DocumentationsEndpoint, query);
        return await GetWithVersionAsync(url, "v1", cancellationToken);
    }

    public async Task<EsiosContentResponse?> GetDocumentationsAsync(EsiosContentQuery? query = null, CancellationToken cancellationToken = default)
    {
        string url = BuildTaxonomyQueryUrl("es", DocumentationsEndpoint, query);
        return await GetFromJsonAsync<EsiosContentResponse>(url, "v1", cancellationToken);
    }

    private string BuildTaxonomyQueryUrl(string language, string endpointBase, EsiosContentQuery? query)
    {
        var endpoint = $"/{language}{endpointBase}";
        var queryParams = new List<string>();

        if (query?.TaxonomyTerms != null)
        {
            foreach (var term in query.TaxonomyTerms)
            {
                queryParams.Add($"taxonomy_terms[]={Uri.EscapeDataString(term)}");
            }
        }

        if (query?.Vocabularies != null)
        {
            foreach (var vocab in query.Vocabularies)
            {
                queryParams.Add($"vocabularies[]={Uri.EscapeDataString(vocab)}");
            }
        }

        if (queryParams.Any())
        {
            endpoint += "?" + string.Join("&", queryParams);
        }

        return endpoint;
    }
}
