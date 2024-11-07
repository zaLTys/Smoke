namespace UI.ViewModels.Requests;

public record ApiRequestDataViewModel(
    string HttpMethod,
    string Url,
    Dictionary<string, string> Headers,
    string? Body,
    object? ExpectedResponse
);
