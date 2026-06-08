using System.Text.Json.Serialization;

namespace IdScan.Example.Models;

public sealed class CreateScanResponse
{
    [JsonPropertyName("ScanRequestId")]
    public Guid ScanRequestId { get; set; }

    [JsonPropertyName("Status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("LanguageCode")]
    public string LanguageCode { get; set; } = string.Empty;

    [JsonPropertyName("ExternalReference")]
    public string ExternalReference { get; set; } = string.Empty;

    [JsonPropertyName("RequestKey")]
    public string RequestKey { get; set; } = string.Empty;

    [JsonPropertyName("ExpiresAtUtc")]
    public DateTimeOffset? ExpiresAtUtc { get; set; }

    [JsonPropertyName("DeepLinkUrl")]
    public string DeepLinkUrl { get; set; } = string.Empty;

    [JsonPropertyName("WebUrl")]
    public string WebUrl { get; set; } = string.Empty;

    [JsonPropertyName("IsTest")]
    public bool IsTest { get; set; }
}
