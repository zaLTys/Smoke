using Domain.Entities.Requests;

public interface IRequestRepository
{
    //Todo Async Await
    ApiRequest GetById(Guid requestId);
    Guid Save(ApiRequest httpRequest);
    ApiRequest Update(ApiRequest httpRequest);

}