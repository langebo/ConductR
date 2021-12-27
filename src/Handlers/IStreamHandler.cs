namespace ConductR;

public interface IStreamHandler<in TQuery, TResult>
{
    IAsyncEnumerable<TResult> HandleAsync(TQuery query, CancellationToken token = default);
}