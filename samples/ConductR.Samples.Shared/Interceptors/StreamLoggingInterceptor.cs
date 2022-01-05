using System.Runtime.CompilerServices;
using ConductR.Dispatchers;
using ConductR.Interceptors;
using Microsoft.Extensions.Logging;

namespace ConductR.Samples.Shared.Interceptors;

public class StreamLoggingInterceptor : StreamInterceptor
{
    private readonly ILogger logger;

    public StreamLoggingInterceptor(IStreamDispatcher dispatcher, ILogger<StreamLoggingInterceptor> logger) : base(dispatcher)
    {
        this.logger = logger;
    }

    public override async IAsyncEnumerable<TResult> DispatchAsync<TStreamQuery, TResult>(TStreamQuery query, [EnumeratorCancellation] CancellationToken token = default)
    {
        logger.LogInformation("Starting to process stream {StreamName}", typeof(TStreamQuery).Name);
        await foreach (var item in Dispatcher.DispatchAsync<TStreamQuery, TResult>(query, token))
        {
            logger.LogDebug("Processing stream item {StreamItem}", item);
            yield return item;
        }
        logger.LogInformation("Finished processing stream {StreamName}", typeof(TStreamQuery).Name);
    }
}
