using ConductR.Types;

namespace ConductR.Handlers;

public interface IStreamHandler<in TStreamQuery, TResult> where TStreamQuery : IStreamQuery
{
    IAsyncEnumerable<TResult> HandleAsync(TStreamQuery query, CancellationToken token = default);
}