using ConductR.Types;

namespace ConductR.Dispatchers;

public interface IStreamDispatcher
{
    IAsyncEnumerable<TResult> DispatchAsync<TStreamQuery, TResult>(TStreamQuery query, CancellationToken token = default) where TStreamQuery : IStreamQuery;
}