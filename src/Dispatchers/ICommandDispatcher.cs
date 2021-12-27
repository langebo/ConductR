namespace ConductR;
public interface ICommandDispatcher
{
    ValueTask<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token = default);
}