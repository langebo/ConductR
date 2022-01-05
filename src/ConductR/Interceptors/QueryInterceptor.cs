using ConductR.Dispatchers;
using ConductR.Types;

namespace ConductR.Interceptors;

public abstract class QueryInterceptor : IQueryDispatcher
{
    protected virtual IQueryDispatcher Dispatcher { get; }
    protected QueryInterceptor(IQueryDispatcher dispatcher) => Dispatcher = dispatcher;

    public abstract ValueTask<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token = default) where TQuery : IQuery;
}