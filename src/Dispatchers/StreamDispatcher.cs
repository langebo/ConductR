using ConductR.Handlers;
using ConductR.Types;
using Microsoft.Extensions.DependencyInjection;

namespace ConductR.Dispatchers;

public class StreamDispatcher : IStreamDispatcher
{
    private readonly IServiceProvider serviceProvider;

    public StreamDispatcher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public IAsyncEnumerable<TResult> DispatchAsync<TStreamQuery, TResult>(TStreamQuery query, CancellationToken token = default) where TStreamQuery : IStreamQuery
    {
        var handler = serviceProvider.GetRequiredService<IStreamHandler<TStreamQuery, TResult>>();
        return handler.HandleAsync(query, token);
    }
}