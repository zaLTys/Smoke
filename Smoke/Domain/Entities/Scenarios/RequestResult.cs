namespace Domain.Entities.Scenarios
{
    public record RequestResult
(
    Guid RequestId,
    object Response,
    bool IsSuccess,
    string ErrorMessage
);
}
