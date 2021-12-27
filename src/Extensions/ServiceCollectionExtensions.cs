using ConductR.Dispatchers;
using ConductR.Handlers;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        services.AddScoped<IStreamDispatcher, StreamDispatcher>();
        services.AddScoped<IEventDispatcher, EventDispatcher>();

        services.AddScoped<IConductor, Conductor>();

        return services;
    }
}