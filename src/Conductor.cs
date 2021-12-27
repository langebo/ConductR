using ConductR.Dispatchers;
using ConductR.Types;
using Microsoft.Extensions.DependencyInjection;

namespace ConductR;

public class Conductor : IConductor
{
    private readonly IServiceProvider serviceProvider;

    public Conductor(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public ValueTask<TResult> CommandAsync<TCommand, TResult>(TCommand command, CancellationToken token = default) where TCommand : ICommand
    {
        var dispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
        return dispatcher.DispatchAsync<TCommand, TResult>(command, token);
    }

    public ValueTask<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken token = default) where TQuery : IQuery
    {
        var dispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
        return dispatcher.DispatchAsync<TQuery, TResult>(query, token);
    }

    public IAsyncEnumerable<TResult> StreamAsync<TStreamQuery, TResult>(TStreamQuery query, CancellationToken token = default) where TStreamQuery : IStreamQuery
    {
        var dispatcher = serviceProvider.GetRequiredService<IStreamDispatcher>();
        return dispatcher.DispatchAsync<TStreamQuery, TResult>(query, token);
    }

    public ValueTask PublishAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IEvent
    {
        var dispatcher = serviceProvider.GetRequiredService<IEventDispatcher>();
        return dispatcher.DispatchAsync(@event);
    }
}
