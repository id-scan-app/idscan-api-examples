using System.Text.Json.Serialization;

namespace IdScan.Example.Models;

public sealed class CreateScanRequest
{
    [JsonPropertyName("templateCode")]
    public string TemplateCode { get; set; } = "default";

    [JsonPropertyName("languageCode")]
    public string LanguageCode { get; set; } = "nl";

    [JsonPropertyName("externalReference")]
    public string ExternalReference { get; set; } = string.Empty;

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; } = string.Empty;

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("requestType")]
    public string RequestType { get; set; } = "identity_check";

    [JsonPropertyName("expiryInHours")]
    public int ExpiryInHours { get; set; } = 48;

    [JsonPropertyName("delivery")]
    public ScanRequestDelivery Delivery { get; set; } = new();

    [JsonPropertyName("templates")]
    public ScanRequestTemplates Templates { get; set; } = new();

    [JsonPropertyName("metadata")]
    public Dictionary<string, string> Metadata { get; set; } = new();

    [JsonPropertyName("isTest")]
    public bool IsTest { get; set; }
}

public sealed class ScanRequestDelivery
{
    [JsonPropertyName("mode")]
    public string Mode { get; set; } = "platform";

    [JsonPropertyName("channels")]
    public List<string> Channels { get; set; } = new() { "sms", "email" };
}

public sealed class ScanRequestTemplates
{
    [JsonPropertyName("smsText")]
    public string SmsText { get; set; } = string.Empty;

    [JsonPropertyName("emailSubject")]
    public string EmailSubject { get; set; } = string.Empty;

    [JsonPropertyName("emailBody")]
    public string EmailBody { get; set; } = string.Empty;

    [JsonPropertyName("preformTitle")]
    public string PreformTitle { get; set; } = string.Empty;

    [JsonPropertyName("preformText")]
    public string PreformText { get; set; } = string.Empty;

    [JsonPropertyName("afterformTitle")]
    public string AfterformTitle { get; set; } = string.Empty;

    [JsonPropertyName("afterformText")]
    public string AfterformText { get; set; } = string.Empty;
}
