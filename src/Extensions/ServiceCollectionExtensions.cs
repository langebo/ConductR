using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ConductR;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConductR(this IServiceCollection services, params Type[] assmeblyMarkerTypes)
    {
        services.Scan(selector => selector
            .FromAssembliesOf(assmeblyMarkerTypes)
            .AddClasses(filter => filter
                .AssignableToAny(typeof(ICommandHandler<,>),
                                 typeof(IQueryHandler<,>),
                                 typeof(IStreamHandler<,>),
                                 typeof(IEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.TryAddScoped<ICommandDispatcher, CommandDispatcher>();
        services.TryAddScoped<IQueryDispatcher, QueryDispatcher>();
        services.TryAddScoped<IStreamDispatcher, StreamDispatcher>();
        services.TryAddScoped<IEventDispatcher, EventDispatcher>();
        services.TryAddScoped<IConductor, Conductor>();

        return services;
    }
}