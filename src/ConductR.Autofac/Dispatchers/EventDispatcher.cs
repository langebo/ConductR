using Autofac;
using ConductR.Dispatchers;
using ConductR.Handlers;
using ConductR.Types;

namespace ConductR.Autofac.Dispatchers;

public class EventDispatcher : IEventDispatcher
{
    private readonly IComponentContext context;

    public EventDispatcher(IComponentContext context) => this.context = context;

    public ValueTask DispatchAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IEvent
    {
        var handler = context.Resolve<IEventHandler<TEvent>>();
        return handler.HandleAsync(@event);
    }
}
