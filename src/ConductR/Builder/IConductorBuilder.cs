using ConductR.Interceptors;

namespace ConductR.Builder;

public interface IConductorBuilder
{
    IConductorBuilder AddCommandInterceptor<TInterceptor>() where TInterceptor : CommandInterceptor;
    IConductorBuilder AddQueryInterceptor<TInterceptor>() where TInterceptor : QueryInterceptor;
    IConductorBuilder AddStreamInterceptor<TInterceptor>() where TInterceptor : StreamInterceptor;
    IConductorBuilder AddEventInterceptor<TInterceptor>() where TInterceptor : EventInterceptor;
}
