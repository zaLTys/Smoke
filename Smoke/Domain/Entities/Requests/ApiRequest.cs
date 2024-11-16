using Domain.Primitives;

namespace Domain.Entities.Requests
{
    public class ApiRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ApiRequestData ApiRequestData { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public RequestType Type { get; set; }

        public ApiRequest(Guid id, string name, ApiRequestData apiRequestData, DateTime createdDate, DateTime modifiedDate, RequestType type = RequestType.HttpRequest)
        {
            Id = id;
            Name = name;
            ApiRequestData = apiRequestData;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            Type = type;
        }
    }

}
