namespace Domain.Entities.Requests
{
    public class RequestResult
    {
        public Guid RequestId { get; set; }
        public string Response { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public RequestResult(Guid requestId, string response, bool isSuccess, string errorMessage)
        {
            RequestId = requestId;
            Response = response;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
    }
}
