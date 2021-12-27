using ConductR.Types;

namespace ConductR.Handlers;

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery
{
    ValueTask<TResult> HandleAsync(TQuery query, CancellationToken token = default);
}