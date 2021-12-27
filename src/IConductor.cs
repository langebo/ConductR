namespace ConductR;

public interface IConductor
{
    ValueTask<TResult> CommandAsync<TCommand, TResult>(TCommand command, CancellationToken token = default);
    ValueTask<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken token = default);
    IAsyncEnumerable<TResult> StreamAsync<TQuery, TResult>(TQuery query, CancellationToken token = default);
    ValueTask PublishAsync<TEvent>(TEvent @event, CancellationToken token = default);
}