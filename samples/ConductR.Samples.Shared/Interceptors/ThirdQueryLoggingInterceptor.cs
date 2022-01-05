using ConductR.Dispatchers;
using ConductR.Interceptors;
using Microsoft.Extensions.Logging;

namespace ConductR.Samples.Shared.Interceptors;

public class ThirdQueryLoggingInterceptor : QueryInterceptor
{
    private readonly ILogger logger;

    public ThirdQueryLoggingInterceptor(IQueryDispatcher dispatcher, ILogger<ThirdQueryLoggingInterceptor> logger) : base(dispatcher)
    {
        this.logger = logger;
    }

    public override async ValueTask<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token = default)
    {
        var result = await Dispatcher.DispatchAsync<TQuery, TResult>(query, token);
        logger.LogInformation("Successfully handled query {QueryName}", typeof(TQuery).Name);
        return result;
    }
}
