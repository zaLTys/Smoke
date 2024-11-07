namespace UI.ViewModels.Requests;

public record ApiRequestViewModel
(
    Guid Id,
    string Name,
    ApiRequestDataViewModel ApiRequestData,
    DateTime CreatedDate,
    DateTime ModifiedDate,
    string Type = "GET"
);
