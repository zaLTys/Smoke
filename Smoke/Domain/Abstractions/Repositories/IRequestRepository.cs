using Domain.Entities.Requests;

public interface IRequestRepository
{
    Guid Save(HttpRequest httpRequest);
    HttpRequest GetById(Guid requestId);
}