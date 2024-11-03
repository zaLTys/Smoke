using Domain.Entities.Requests;

namespace Application.Abstractions.Services
{
    public interface IPlaceholderReplacer
    {
        ApiRequest ReplacePlaceholders(ApiRequest apiRequest, IDictionary<string, string> parameters);
    }
}
