using System.Reflection;
using ConductR.Builder;
using ConductR.Dispatchers;
using ConductR.Handlers;
using ConductR.Microsoft.Dispatchers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrutor;

namespace ConductR.Microsoft;

public static class ServiceCollectionExtensions
{
    public static IConductorBuilder AddConductR(this IServiceCollection services, params Type[] assemblyMarkers) =>
        services.AddConductR(ServiceLifetime.Scoped, assemblyMarkers);

    public static IConductorBuilder AddConductR(this IServiceCollection services, params Assembly[] assemblies) =>
        services.AddConductR(ServiceLifetime.Scoped, assemblies);

    public static IConductorBuilder AddConductR(this IServiceCollection services, ServiceLifetime lifetime, params Type[] assemblyMarkers) =>
        services.AddConductR(lifetime, assemblyMarkers.Select(t => t.Assembly).ToArray());

    public static IConductorBuilder AddConductR(this IServiceCollection services, ServiceLifetime lifetime, params Assembly[] assemblies)
    {
        services.Scan(selector => selector
            .FromAssemblies(assemblies)
            .AddClasses(filter => filter
                .AssignableToAny(typeof(ICommandHandler<,>),
                                 typeof(IQueryHandler<,>),
                                 typeof(IStreamHandler<,>),
                                 typeof(IEventHandler<>)))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithLifetime(lifetime));

        services.TryAdd(ServiceDescriptor.Describe(typeof(ICommandDispatcher), typeof(CommandDispatcher), lifetime));
        services.TryAdd(ServiceDescriptor.Describe(typeof(IQueryDispatcher), typeof(QueryDispatcher), lifetime));
        services.TryAdd(ServiceDescriptor.Describe(typeof(IStreamDispatcher), typeof(StreamDispatcher), lifetime));
        services.TryAdd(ServiceDescriptor.Describe(typeof(IEventDispatcher), typeof(EventDispatcher), lifetime));
        services.TryAdd(ServiceDescriptor.Describe(typeof(IConductor), typeof(Conductor), lifetime));

        return new ConductorBuilder(services);
    }
}