using Domain.Entities.Requests;

public interface IRequestRepository
{
    Guid Save(ApiRequest httpRequest);
    ApiRequest GetById(Guid requestId);
}