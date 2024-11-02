using Domain.Entities.Requests;

public interface ICurlParserService
{
    ApiRequest ParseCurlCommand(string name, string curlCommand);
}