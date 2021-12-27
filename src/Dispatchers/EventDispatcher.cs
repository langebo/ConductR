using Microsoft.Extensions.DependencyInjection;

namespace ConductR;

public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public ValueTask DispatchAsync<TEvent>(TEvent @event, CancellationToken token = default)
    {
        var handler = serviceProvider.GetRequiredService<IEventHandler<TEvent>>();
        return handler.HandleAsync(@event);
    }
}
