namespace Domain.Entities.Requests
{
    public record RequestResult
(
    Guid RequestId,
    string Response,
    bool IsSuccess,
    string ErrorMessage
);
}
