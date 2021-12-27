using Microsoft.Extensions.DependencyInjection;

namespace ConductR;

public class Conductor : IConductor
{
    private readonly IServiceProvider serviceProvider;

    public Conductor(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public ValueTask<TResult> CommandAsync<TCommand, TResult>(TCommand command, CancellationToken token = default)
    {
        var dispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
        return dispatcher.DispatchAsync<TCommand, TResult>(command, token);
    }

    public ValueTask<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken token = default)
    {
        var dispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
        return dispatcher.DispatchAsync<TQuery, TResult>(query, token);
    }

    public IAsyncEnumerable<TResult> StreamAsync<TQuery, TResult>(TQuery query, CancellationToken token = default)
    {
        var dispatcher = serviceProvider.GetRequiredService<IStreamDispatcher>();
        return dispatcher.DispatchAsync<TQuery, TResult>(query, token);
    }

    public ValueTask PublishAsync<TEvent>(TEvent @event, CancellationToken token = default)
    {
        var dispatcher = serviceProvider.GetRequiredService<IEventDispatcher>();
        return dispatcher.DispatchAsync(@event);
    }
}
