using Autofac;
using ConductR.Dispatchers;
using ConductR.Handlers;
using ConductR.Types;

namespace ConductR.Autofac.Dispatchers;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IComponentContext context;

    public QueryDispatcher(IComponentContext context) => this.context = context;

    public ValueTask<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token) where TQuery : IQuery
    {
        var handler = context.Resolve<IQueryHandler<TQuery, TResult>>();
        return handler.HandleAsync(query, token);
    }
}