using Domain.Primitives;

namespace Domain.Entities.Requests
{
    public class DbRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DbRequestData DbRequestData { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public RequestType Type { get; set; }

        public DbRequest(Guid id, string name, DbRequestData dbRequestData, DateTime createdDate, DateTime modifiedDate, RequestType type = RequestType.DbRequest)
        {
            Id = id;
            Name = name;
            DbRequestData = dbRequestData;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            Type = type;
        }
    }

}
