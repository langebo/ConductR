namespace ConductR;

public interface IStreamDispatcher
{
    IAsyncEnumerable<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token = default);
}