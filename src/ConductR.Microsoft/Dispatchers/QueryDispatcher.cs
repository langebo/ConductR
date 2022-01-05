using ConductR.Dispatchers;
using ConductR.Handlers;
using ConductR.Types;
using Microsoft.Extensions.DependencyInjection;

namespace ConductR.Microsoft.Dispatchers;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider) => this.serviceProvider = serviceProvider;

    public ValueTask<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token) where TQuery : IQuery
    {
        var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return handler.HandleAsync(query, token);
    }
}