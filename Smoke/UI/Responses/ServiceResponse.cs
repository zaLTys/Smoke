namespace UI.Responses
{
    public class ServiceResponse : IServiceResponse
    {
        public ServiceResponse()
        {
            //for deserialization
        }

        public bool Success { get; set; }
        public string? Message { get; set; }

        public static ServiceResponse Error(string message) => new ServiceResponse { Success = false, Message = message };
    }

    public class ServiceResponse<T> : ServiceResponse, IServiceResponse
    {
        bool IServiceResponse.Success => Success;
        string IServiceResponse.Message => Message;

        public ServiceResponse()
        {
            //for deserialization
        }

        public T Data { get; set; } = default!;


        public static new ServiceResponse<T> Error(string message) => new ServiceResponse<T> { Success = false, Message = message };
        public static ServiceResponse<T> FromData(T data) => new ServiceResponse<T> { Success = true, Data = data };
        public static ServiceResponse<T> FromData(T data, string message) => new ServiceResponse<T> { Success = true, Data = data, Message = message };
    }
}
