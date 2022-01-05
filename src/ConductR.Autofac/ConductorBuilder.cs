using Autofac;
using ConductR.Builder;
using ConductR.Dispatchers;
using ConductR.Interceptors;

namespace ConductR.Autofac;

public class ConductorBuilder : IConductorBuilder
{
    private readonly ContainerBuilder containerBuilder;

    internal ConductorBuilder(ContainerBuilder containerBuilder)
    {
        this.containerBuilder = containerBuilder;
    }

    public IConductorBuilder AddCommandInterceptor<TInterceptor>() where TInterceptor : CommandInterceptor
    {
        containerBuilder.RegisterDecorator<TInterceptor, ICommandDispatcher>();
        return this;
    }

    public IConductorBuilder AddQueryInterceptor<TInterceptor>() where TInterceptor : QueryInterceptor
    {
        containerBuilder.RegisterDecorator<TInterceptor, IQueryDispatcher>();
        return this;
    }

    public IConductorBuilder AddStreamInterceptor<TInterceptor>() where TInterceptor : StreamInterceptor
    {
        containerBuilder.RegisterDecorator<TInterceptor, IStreamDispatcher>();
        return this;
    }

    public IConductorBuilder AddEventInterceptor<TInterceptor>() where TInterceptor : EventInterceptor
    {
        containerBuilder.RegisterDecorator<TInterceptor, IEventDispatcher>();
        return this;
    }
}
