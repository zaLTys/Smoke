using System.Text;
using System.Text.RegularExpressions;

namespace WebApi.Middleware;

public class CurlStringEscapeMiddleware
{
    private readonly RequestDelegate _next;

    public CurlStringEscapeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if ((context.Request.Path == "/api/Requests/create" || context.Request.Path == "/api/Requests/testExecute")
            && context.Request.Method == HttpMethods.Post)
        {
            // Read the original request body
            using var reader = new StreamReader(context.Request.Body);
            var originalContent = await reader.ReadToEndAsync();

            // Perform escaping
            var mutatedContent = EscapeCurlCommand(originalContent);

            // Replace the request body with the mutated content as a JSON-escaped string
            var byteArray = Encoding.UTF8.GetBytes(mutatedContent);
            context.Request.Body = new MemoryStream(byteArray);

            // Reset the position of the request body stream to make it readable again downstream
            context.Request.Body.Seek(0, SeekOrigin.Begin);
        }

        await _next(context); // Pass control to the next middleware or controller
    }

    private string EscapeCurlCommand(string curlCommand)
    {
        if (IsAlreadyEscaped(curlCommand))
        {
            return curlCommand; // Return as is to avoid double-escaping
        }

        // Escape backslashes
        curlCommand = curlCommand.Replace("\\", "\\\\");

        // Escape double quotes
        curlCommand = curlCommand.Replace("\"", "\\\"");

        // Escape newlines
        curlCommand = curlCommand.Replace("\n", "\\n").Replace("\r", "\\r");

        // Escape URLs in curl syntax
        curlCommand = Regex.Replace(curlCommand, "(https?://)", @"https:\/\/");

        // Wrap the entire command in JSON string format to ensure compatibility
        return $"\"{curlCommand}\"";
    }

    private bool IsAlreadyEscaped(string input)
    {
        return input.Contains("\\\"") || input.Contains("\\n") || input.Contains("\\\\");
    }

}