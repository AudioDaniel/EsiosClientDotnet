using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EsiosClient.Models;

public class EsiosAuction
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class EsiosAuctionResponse
{
    [JsonPropertyName("auctions")]
    public List<EsiosAuction> Auctions { get; set; } = new();
}
