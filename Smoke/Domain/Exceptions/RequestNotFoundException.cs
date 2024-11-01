using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class RequestNotFoundException : NotFoundException
{
    public RequestNotFoundException(Guid requestId)
        : base($"The request with the identifier {requestId} was not found.")
    {
    }
}