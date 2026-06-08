using IdScan.Example.Models;
using IdScan.Example.Services;

var baseUrl = Environment.GetEnvironmentVariable("IDSCAN_API_BASE_URL")
              ?? "https://api.id-scan.app/";


// Set IDSCAN_API_KEY in your environment instead.
var apiKey = Environment.GetEnvironmentVariable("IDSCAN_API_KEY");

if (string.IsNullOrWhiteSpace(apiKey))
{
    Console.WriteLine("Missing API key.");
    Console.WriteLine("Get your API key from the ID Scan Portal.");
    Console.WriteLine("Navigate to Settings > API & Integrations.");
    Console.WriteLine("Then set the IDSCAN_API_KEY environment variable before running this example.");
    return;
}

using var httpClient = new HttpClient
{
    BaseAddress = new Uri(baseUrl)
};

httpClient.DefaultRequestHeaders.Add("X-API-Key", apiKey);

var client = new IdScanClient(httpClient);

// TemplateCode is optional.
// When provided, the requested fields and verification checks are determined
// by the template configuration in the ID Scan Portal.
// Without a template, requested fields and checks can be supplied directly
// in the API request.

var request = new CreateScanRequest
{
    TemplateCode = "default",
    LanguageCode = "en",
    ExternalReference = string.Empty,
    FirstName = "Leo",
    LastName = "Bork",
    EmailAddress = "customer@gmail.com",
    PhoneNumber = "31600000000",
    RequestType = "identity_check",
    ExpiryInHours = 48,
    Delivery = new ScanRequestDelivery
    {
        Mode = "platform",
        Channels = new List<string> { "sms", "email" }
    },
    Templates = new ScanRequestTemplates
    {
        SmsText = string.Empty,
        EmailSubject = string.Empty,
        EmailBody = string.Empty,
        PreformTitle = string.Empty,
        PreformText = string.Empty,
        AfterformTitle = string.Empty,
        AfterformText = string.Empty
    },
    Metadata = new Dictionary<string, string>
    {
        ["key_0"] = "string"
    },
    IsTest = false
};

Console.WriteLine("Creating identity verification request...");

try
{
  var created = await client.CreateScanRequestAsync(request);

  Console.WriteLine();
  Console.WriteLine("Request created successfully.");
  Console.WriteLine($"ScanRequestId: {created.ScanRequestId}");
  Console.WriteLine($"Status: {created.Status}");
  Console.WriteLine($"LanguageCode: {created.LanguageCode}");
  Console.WriteLine($"RequestKey: {created.RequestKey}");
  Console.WriteLine($"ExpiresAtUtc: {created.ExpiresAtUtc:O}");
  Console.WriteLine($"DeepLinkUrl: {created.DeepLinkUrl}");
  Console.WriteLine($"WebUrl: {created.WebUrl}");

  Console.WriteLine();
  Console.WriteLine("Open the verification link and complete the ID scan.");
  Console.WriteLine("Press Enter when the verification is completed.");
  Console.ReadLine();

  Console.WriteLine("Retrieving current request status...");

  var result = await client.GetScanResultAsync(created.ScanRequestId.ToString());


  var parsedResultJson = result.ParseResultJson();

    Console.WriteLine();
    Console.WriteLine("Verification result");
    Console.WriteLine($"VerificationRequestId: {result.VerificationRequestId}");
    Console.WriteLine($"Name: {result.Name}");
    Console.WriteLine($"Status: {result.Status}");
    Console.WriteLine($"RequestType: {result.RequestType}");
    Console.WriteLine($"Summary: {result.Summary}");
    Console.WriteLine($"DocumentType: {result.DocumentType}");
    Console.WriteLine($"HasResult: {result.HasResult}");
    Console.WriteLine($"IsSuccess: {result.IsSuccess}");
    Console.WriteLine($"NfcReadSuccess: {result.NfcReadSuccess}");
    Console.WriteLine($"ChipAuthenticationSuccess: {result.ChipAuthenticationSuccess}");
    Console.WriteLine($"CompletedDateTimeUtc: {result.CompletedDateTimeUtc:O}");

    if (parsedResultJson?.Fields is not null)
    {
        Console.WriteLine();
        Console.WriteLine("Returned fields");
        Console.WriteLine($"Given names: {parsedResultJson.Fields.GivenNames}");
        Console.WriteLine($"Surname: {parsedResultJson.Fields.Surname}");
        Console.WriteLine($"Document number: {parsedResultJson.Fields.DocumentNumber}");
        Console.WriteLine($"Nationality: {parsedResultJson.Fields.Nationality}");
        Console.WriteLine($"Country of issue: {parsedResultJson.Fields.CountryOfIssue}");
        Console.WriteLine($"Date of birth: {parsedResultJson.Fields.DateOfBirth}");
        Console.WriteLine($"Date of expiry: {parsedResultJson.Fields.DateOfExpiry}");
    }

    if (parsedResultJson?.Checks is not null)
    {
        Console.WriteLine();
        Console.WriteLine("Checks from ResultJson");
        Console.WriteLine($"Chip authentication success: {parsedResultJson.Checks.ChipAuthenticationSuccess}");
        Console.WriteLine($"NFC read success: {parsedResultJson.Checks.NfcReadSuccess}");
        Console.WriteLine($"Passive authentication success: {parsedResultJson.Checks.PassiveAuthenticationSuccess}");
        Console.WriteLine($"Selfie check success: {parsedResultJson.Checks.SelfieCheckSuccess}");
    }
}
catch (HttpRequestException ex)
{
    Console.WriteLine("The API request failed.");
    Console.WriteLine(ex.Message);
}
catch (Exception ex)
{
    Console.WriteLine("Unexpected error.");
    Console.WriteLine(ex.Message);
}
