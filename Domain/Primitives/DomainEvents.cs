using MediatR;

namespace Domain.Primitives
{
    public record DomainEvents(Guid Id) : INotification;
}
