using System.Text.Json.Serialization;

namespace EsiosClient.Models;

public class EsiosVocabulary
{
    [JsonPropertyName("id_vocabulary")]
    public int IdVocabulary { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
