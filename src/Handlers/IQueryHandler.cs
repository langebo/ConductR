namespace ConductR;
public interface IQueryHandler<in TQuery, TResult>
{
    ValueTask<TResult> HandleAsync(TQuery query, CancellationToken token = default);
}