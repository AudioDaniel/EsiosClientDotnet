using System;
using System.Text.Json.Serialization;

namespace EsiosClient.Models;

public class EsiosWidget
{
    [JsonPropertyName("id_widget")]
    public int IdWidget { get; set; }

    [JsonPropertyName("cache_key_date")]
    public DateTime? CacheKeyDate { get; set; }
}

public class EsiosWidgetResponse
{
    [JsonPropertyName("widget")]
    public EsiosWidget? Widget { get; set; }
}
