using System.Text.Json.Serialization;

namespace EsiosClient.Models;

public class EsiosTaxonomyTerm
{
    [JsonPropertyName("id_taxonomy_term")]
    public int IdTaxonomyTerm { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("vocabulary_id")]
    public int VocabularyId { get; set; }
}
