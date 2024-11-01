using Domain.Entities.Requests;

public interface ICurlParserService
{
    ApiRequest ParseCurlCommand(string curlCommand);
}