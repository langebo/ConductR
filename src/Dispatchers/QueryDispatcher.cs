using Microsoft.Extensions.DependencyInjection;

namespace ConductR;
public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public ValueTask<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token)
    {
        var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return handler.HandleAsync(query, token);
    }
}