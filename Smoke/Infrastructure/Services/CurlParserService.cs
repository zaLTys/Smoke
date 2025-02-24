﻿using Domain.Entities.Requests;
using Domain.Primitives;
using System.Text;
using System.Text.RegularExpressions;
public class CurlParserService : ICurlParserService
{
    public ApiRequest ParseCurlCommand(string name, string curlCommand)
    {
        if (string.IsNullOrWhiteSpace(curlCommand))
        {
            throw new ArgumentException("Curl command cannot be null or empty.");
        }

        curlCommand = Regex.Replace(curlCommand, @"\\[r]?\\n", " ");

        curlCommand = curlCommand.Replace("\\\n", " ").Replace("\\\r\n", " ").Replace("\\", "");

        var tokens = Tokenize(curlCommand);
        var headers = new Dictionary<string, string>();
        string url = null;
        string body = null;
        HttpMethodType method = HttpMethodType.GET;

        for (int i = 0; i < tokens.Count; i++)
        {
            string token = tokens[i];

            if (token == "curl")
            {
                continue;
            }
            else if (token == "-X" || token == "--request")
            {
                if (i + 1 < tokens.Count)
                {
                    method = ParseHttpMethod(tokens[++i]);
                }
                else
                {
                    throw new ArgumentException("Invalid curl command: Missing method after -X/--request.");
                }
            }
            else if (token == "-H" || token == "--header")
            {
                if (i + 1 < tokens.Count)
                {
                    var header = RemoveQuotes(tokens[++i]);
                    var separatorIndex = header.IndexOf(':');
                    if (separatorIndex > 0)
                    {
                        var headerName = header.Substring(0, separatorIndex).Trim();
                        var headerValue = header.Substring(separatorIndex + 1).Trim();
                        headers[headerName] = headerValue;
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid header format: {header}");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid curl command: Missing header value after -H/--header.");
                }
            }
            else if (token == "-d" || token == "--data" || token == "--data-raw" || token == "--data-binary")
            {
                if (i + 1 < tokens.Count)
                {
                    body = RemoveQuotes(tokens[++i]);
                    // Change method to POST if not explicitly set
                    if (method == HttpMethodType.GET)
                    {
                        method = HttpMethodType.POST;
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid curl command: Missing data after -d/--data.");
                }
            }
            else if (token.StartsWith("-"))
            {
                continue;
            }
            else
            {
                if (url == null)
                {
                    url = RemoveQuotes(token);
                }
                else
                {
                    throw new ArgumentException($"Unexpected token: {token}");
                }
            }
        }

        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentException("Invalid curl command: URL not found.");
        }

        var apiRequestData = new ApiRequestData(method, url, headers, body, null);

        return new ApiRequest(Guid.NewGuid(), name, apiRequestData, DateTime.UtcNow, DateTime.UtcNow);
    }

    private List<string> Tokenize(string input)
    {
        var tokens = new List<string>();
        var sb = new StringBuilder();
        bool inSingleQuote = false;
        bool inDoubleQuote = false;
        bool escapeNext = false;

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (escapeNext)
            {
                sb.Append(c);
                escapeNext = false;
            }
            else if (c == '\\')
            {
                escapeNext = true;
            }
            else if (c == '\'' && !inDoubleQuote)
            {
                inSingleQuote = !inSingleQuote;
                sb.Append(c); // Keep quotes for later removal
            }
            else if (c == '"' && !inSingleQuote)
            {
                inDoubleQuote = !inDoubleQuote;
                sb.Append(c); // Keep quotes for later removal
            }
            else if (char.IsWhiteSpace(c) && !inSingleQuote && !inDoubleQuote)
            {
                if (sb.Length > 0)
                {
                    tokens.Add(sb.ToString());
                    sb.Clear();
                }
            }
            else
            {
                sb.Append(c);
            }
        }

        if (sb.Length > 0)
        {
            tokens.Add(sb.ToString());
        }

        return tokens;
    }

    private string RemoveQuotes(string input)
    {
        if ((input.StartsWith("\"") && input.EndsWith("\"")) ||
            (input.StartsWith("'") && input.EndsWith("'")))
        {
            return input.Substring(1, input.Length - 2);
        }
        return input;
    }

    private HttpMethodType ParseHttpMethod(string method)
    {
        if (Enum.TryParse<HttpMethodType>(method, true, out var result))
        {
            return result;
        }
        else
        {
            throw new ArgumentException($"Invalid HTTP method: {method}");
        }
    }
}