using ConductR.Dispatchers;
using ConductR.Handlers;
using ConductR.Types;
using Microsoft.Extensions.DependencyInjection;

namespace ConductR.Microsoft.Dispatchers;

public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider) => this.serviceProvider = serviceProvider;

    public ValueTask DispatchAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IEvent
    {
        var handler = serviceProvider.GetRequiredService<IEventHandler<TEvent>>();
        return handler.HandleAsync(@event);
    }
}
