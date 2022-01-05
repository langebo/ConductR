using ConductR.Dispatchers;
using ConductR.Types;

namespace ConductR.Interceptors;

public abstract class EventInterceptor : IEventDispatcher
{
    protected virtual IEventDispatcher Dispatcher { get; }
    protected EventInterceptor(IEventDispatcher dispatcher) => Dispatcher = dispatcher;

    public abstract ValueTask DispatchAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IEvent;
}