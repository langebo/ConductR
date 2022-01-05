using ConductR.Types;

namespace ConductR.Handlers;

public interface IEventHandler<TEvent> where TEvent : IEvent
{
    ValueTask HandleAsync(TEvent @event, CancellationToken token = default);
}