using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EsiosClient.Models;

public class EsiosArchiveDownload
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class EsiosArchive
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("horizon")]
    public string Horizon { get; set; } = string.Empty;

    [JsonPropertyName("archive_type")]
    public string ArchiveType { get; set; } = string.Empty;

    [JsonPropertyName("download")]
    public EsiosArchiveDownload? Download { get; set; }

    [JsonPropertyName("date_times")]
    public List<DateTime>? DateTimes { get; set; }

    [JsonPropertyName("publication_date")]
    public List<DateTime>? PublicationDate { get; set; }

    [JsonPropertyName("taxonomy_terms")]
    public List<EsiosTaxonomyTerm>? TaxonomyTerms { get; set; }

    [JsonPropertyName("vocabularies")]
    public List<EsiosVocabulary>? Vocabularies { get; set; }
}

public class EsiosArchiveResponse
{
    [JsonPropertyName("archives")]
    public List<EsiosArchive> Archives { get; set; } = new();
}
