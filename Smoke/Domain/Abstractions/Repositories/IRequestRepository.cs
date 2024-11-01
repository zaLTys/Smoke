using Domain.Entities.Requests;

public interface IRequestRepository
{
    //Todo Async Await
    IEnumerable<ApiRequest> GetAll();
    ApiRequest GetById(Guid requestId);
    Guid Save(ApiRequest httpRequest);
    ApiRequest Update(ApiRequest httpRequest);

}