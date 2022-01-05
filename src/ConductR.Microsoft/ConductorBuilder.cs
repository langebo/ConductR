using ConductR.Builder;
using ConductR.Dispatchers;
using ConductR.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace ConductR.Microsoft;

public class ConductorBuilder : IConductorBuilder
{
    private readonly IServiceCollection services;

    internal ConductorBuilder(IServiceCollection services)
    {
        this.services = services;
    }

    public IConductorBuilder AddCommandInterceptor<TInterceptor>() where TInterceptor : CommandInterceptor
    {
        services.Decorate<ICommandDispatcher, TInterceptor>();
        return this;
    }

    public IConductorBuilder AddQueryInterceptor<TInterceptor>() where TInterceptor : QueryInterceptor
    {
        services.Decorate<IQueryDispatcher, TInterceptor>();
        return this;
    }

    public IConductorBuilder AddStreamInterceptor<TInterceptor>() where TInterceptor : StreamInterceptor
    {
        services.Decorate<IStreamDispatcher, TInterceptor>();
        return this;
    }

    public IConductorBuilder AddEventInterceptor<TInterceptor>() where TInterceptor : EventInterceptor
    {
        services.Decorate<IEventDispatcher, TInterceptor>();
        return this;
    }
}
