using ConductR.Dispatchers;
using ConductR.Types;

namespace ConductR.Interceptors;

public abstract class StreamInterceptor : IStreamDispatcher
{
    protected virtual IStreamDispatcher Dispatcher { get; }
    protected StreamInterceptor(IStreamDispatcher dispatcher) => Dispatcher = dispatcher;

    public abstract IAsyncEnumerable<TResult> DispatchAsync<TStreamQuery, TResult>(TStreamQuery query, CancellationToken token = default) where TStreamQuery : IStreamQuery;
}