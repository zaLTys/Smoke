using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class DealNotFoundException : NotFoundException
{
    public DealNotFoundException(string dealId)
        : base($"The deal with the identifier {dealId} was not found.")
    {
    }
}