using Autofac;
using ConductR.Dispatchers;
using ConductR.Handlers;
using ConductR.Types;

namespace ConductR.Autofac.Dispatchers;

public class StreamDispatcher : IStreamDispatcher
{
    private readonly IComponentContext context;

    public StreamDispatcher(IComponentContext context) => this.context = context;

    public IAsyncEnumerable<TResult> DispatchAsync<TStreamQuery, TResult>(TStreamQuery query, CancellationToken token = default) where TStreamQuery : IStreamQuery
    {
        var handler = context.Resolve<IStreamHandler<TStreamQuery, TResult>>();
        return handler.HandleAsync(query, token);
    }
}