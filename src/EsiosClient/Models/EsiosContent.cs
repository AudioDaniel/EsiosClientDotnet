using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EsiosClient.Models;

public class EsiosContentMeta
{
    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("cache_date")]
    public string? CacheDate { get; set; }
}

public class EsiosContent
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;

    [JsonPropertyName("published_status")]
    public string PublishedStatus { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("published_at")]
    public DateTime? PublishedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("permalink")]
    public string Permalink { get; set; } = string.Empty;

    [JsonPropertyName("taxonomy_terms")]
    public List<EsiosTaxonomyTerm>? TaxonomyTerms { get; set; }

    [JsonPropertyName("vocabularies")]
    public List<EsiosVocabulary>? Vocabularies { get; set; }
}

public class EsiosContentResponse
{
    [JsonPropertyName("contents")]
    public List<EsiosContent> Contents { get; set; } = new();

    [JsonPropertyName("meta")]
    public EsiosContentMeta? Meta { get; set; }
}
