using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EsiosClient.Models;

public class EsiosIndicator
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}

public class EsiosIndicatorResponse
{
    [JsonPropertyName("indicators")]
    public List<EsiosIndicator> Indicators { get; set; } = new();
}
