using Microsoft.Extensions.DependencyInjection;

namespace ConductR;

public class StreamDispatcher : IStreamDispatcher
{
    private readonly IServiceProvider serviceProvider;

    public StreamDispatcher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public IAsyncEnumerable<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token = default)
    {
        var handler = serviceProvider.GetRequiredService<IStreamHandler<TQuery, TResult>>();
        return handler.HandleAsync(query, token);
    }
}