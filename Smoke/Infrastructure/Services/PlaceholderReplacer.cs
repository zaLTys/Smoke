using Application.Abstractions.Services;
using Domain.Entities.Requests;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class PlaceholderReplacer : IPlaceholderReplacer
    {
        public ApiRequest ReplacePlaceholders(ApiRequest apiRequest, IDictionary<string, string> sharedData)
        {
            var requestData = apiRequest.ApiRequestData;

            var updatedUrl = ReplacePlaceholdersInString(requestData.Url, sharedData);
            var updatedHeaders = ReplacePlaceholdersInDictionary(requestData.Headers, sharedData);
            var updatedBody = ReplacePlaceholdersInString(requestData.Body, sharedData);
            var updatedExpectedResponse = ReplacePlaceholdersInObject(requestData.ExpectedResponse, sharedData);

            requestData.Url = updatedUrl;
            requestData.Headers = updatedHeaders;
            requestData.Body = updatedBody;
            requestData.ExpectedResponse = updatedExpectedResponse;

            apiRequest.ApiRequestData = requestData;

            return apiRequest;
        }


        private string ReplacePlaceholdersInString(string input, IDictionary<string, string> parameters)
        {
            if (string.IsNullOrEmpty(input)) return input;

            foreach (var param in parameters)
            {
                input = input.Replace($"{{{{{param.Key}}}}}", param.Value?.ToString());
            }
            return input;
        }

        private Dictionary<string, string> ReplacePlaceholdersInDictionary(Dictionary<string, string> input, IDictionary<string, string> parameters)
        {
            if (input == null) return null;

            var result = new Dictionary<string, string>();
            foreach (var kvp in input)
            {
                result[kvp.Key] = ReplacePlaceholdersInString(kvp.Value, parameters);
            }
            return result;
        }

        private object ReplacePlaceholdersInObject(object input, IDictionary<string, string> parameters)
        {
            if (input == null) return null;

            var jsonString = JsonConvert.SerializeObject(input);
            jsonString = ReplacePlaceholdersInString(jsonString, parameters);
            return JsonConvert.DeserializeObject(jsonString);
        }
    }

}
