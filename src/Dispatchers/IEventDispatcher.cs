using ConductR.Types;

namespace ConductR.Dispatchers;

public interface IEventDispatcher
{
    ValueTask DispatchAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IEvent;
}