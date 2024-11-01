using Domain.Entities.Requests;
using Domain.Primitives;
using System.Text.RegularExpressions;

public class CurlParserService : ICurlParserService
{
    public HttpRequest ParseCurlCommand(string curlCommand)
    {
        HttpMethodType method = HttpMethodType.GET;
        string url = string.Empty;
        var headers = new Dictionary<string, string>();
        string body = string.Empty;

        var methodRegex = new Regex(@"-X\s+(?<method>[A-Z]+)", RegexOptions.IgnoreCase);
        var urlRegex = new Regex(@"curl\s+'?(?<url>https?://[^\s]+)'?", RegexOptions.IgnoreCase);
        var headerRegex = new Regex(@"-H\s+'(?<header>[^:]+):\s*(?<value>[^']+)'", RegexOptions.IgnoreCase);
        var dataRegex = new Regex(@"--data\s+'(?<data>[^']+)'", RegexOptions.IgnoreCase);

        var urlMatch = urlRegex.Match(curlCommand);
        if (urlMatch.Success)
        {
            url = urlMatch.Groups["url"].Value;
        }
        else
        {
            throw new ArgumentException("Invalid curl command: URL not found.");
        }

        var methodMatch = methodRegex.Match(curlCommand);
        if (methodMatch.Success && Enum.TryParse(methodMatch.Groups["method"].Value.ToUpper(), out HttpMethodType parsedMethod))
        {
            method = parsedMethod;
        }

        foreach (Match headerMatch in headerRegex.Matches(curlCommand))
        {
            string header = headerMatch.Groups["header"].Value;
            string value = headerMatch.Groups["value"].Value;
            headers[header] = value;
        }

        var dataMatch = dataRegex.Match(curlCommand);
        if (dataMatch.Success)
        {
            body = dataMatch.Groups["data"].Value;
        }

        return new HttpRequest(
            Id: Guid.NewGuid(),
            Name: "ParsedRequest",
            HttpMethod: method,
            Url: url,
            Headers: headers,
            Body: body,
            ExpectedResponse: null,
            CreatedDate: DateTime.UtcNow,
            ModifiedDate: DateTime.UtcNow
        );
    }
}
