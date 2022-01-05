using ConductR.Types;

namespace ConductR;

public interface IConductor
{
    ValueTask<TResult> CommandAsync<TCommand, TResult>(TCommand command, CancellationToken token = default) where TCommand : ICommand;
    ValueTask<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken token = default) where TQuery : IQuery;
    IAsyncEnumerable<TResult> StreamAsync<TStreamQuery, TResult>(TStreamQuery query, CancellationToken token = default) where TStreamQuery : IStreamQuery;
    ValueTask PublishAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IEvent;
}