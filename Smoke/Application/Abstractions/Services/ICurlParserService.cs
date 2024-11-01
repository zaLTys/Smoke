using Domain.Entities.Requests;

public interface ICurlParserService
{
    HttpRequest ParseCurlCommand(string curlCommand);
}