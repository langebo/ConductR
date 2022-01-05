using ConductR.Dispatchers;
using ConductR.Types;

namespace ConductR;

public class Conductor : IConductor
{
    private readonly ICommandDispatcher commandDispatcher;
    private readonly IQueryDispatcher queryDispatcher;
    private readonly IStreamDispatcher streamDispatcher;
    private readonly IEventDispatcher eventDispatcher;

    public Conductor(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IStreamDispatcher streamDispatcher, IEventDispatcher eventDispatcher)
    {
        this.commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
        this.queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        this.streamDispatcher = streamDispatcher ?? throw new ArgumentNullException(nameof(streamDispatcher));
        this.eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
    }

    public ValueTask<TResult> CommandAsync<TCommand, TResult>(TCommand command, CancellationToken token = default) where TCommand : ICommand =>
        commandDispatcher.DispatchAsync<TCommand, TResult>(command, token);

    public ValueTask<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken token = default) where TQuery : IQuery =>
        queryDispatcher.DispatchAsync<TQuery, TResult>(query, token);

    public IAsyncEnumerable<TResult> StreamAsync<TStreamQuery, TResult>(TStreamQuery query, CancellationToken token = default) where TStreamQuery : IStreamQuery =>
        streamDispatcher.DispatchAsync<TStreamQuery, TResult>(query, token);

    public ValueTask PublishAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IEvent =>
        eventDispatcher.DispatchAsync(@event);
}
