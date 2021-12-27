using ConductR.Types;

namespace ConductR.Dispatchers;

public interface IQueryDispatcher
{
    ValueTask<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token = default) where TQuery : IQuery;
}