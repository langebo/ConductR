namespace ConductR;
public interface IQueryDispatcher
{
    ValueTask<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token = default);
}