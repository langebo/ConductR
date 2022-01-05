using System.Reflection;
using Autofac;
using Autofac.Builder;
using ConductR.Builder;

namespace ConductR.Autofac;

public static class ContainerBuilderExtensions
{
    public static IConductorBuilder AddConductR(this ContainerBuilder builder, params Type[] assemblyMarkers) =>
        builder.AddConductR(AutofacLifetime.PerLifetimeScope, assemblyMarkers);

    public static IConductorBuilder AddConductR(this ContainerBuilder builder, params Assembly[] assemblies) =>
        builder.AddConductR(AutofacLifetime.PerLifetimeScope, assemblies);

    public static IConductorBuilder AddConductR(this ContainerBuilder builder, AutofacLifetime lifetime, params Type[] assemblyMarkers) =>
        builder.AddConductR(lifetime, assemblyMarkers.Select(t => t.Assembly).ToArray());

    public static IConductorBuilder AddConductR(this ContainerBuilder builder, AutofacLifetime lifetime, params Assembly[] assemblies)
    {
        builder.RegisterModule(new ConductorModule(lifetime, assemblies));
        return new ConductorBuilder(builder);
    }

    internal static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InstancePer<TLimit, TActivatorData, TStyle>(
        this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder, AutofacLifetime lifetime) => lifetime switch
        {
            AutofacLifetime.SingleInstance => builder.SingleInstance(),
            AutofacLifetime.PerLifetimeScope => builder.InstancePerLifetimeScope(),
            AutofacLifetime.PerDependency => builder.InstancePerDependency(),
            _ => throw new ArgumentOutOfRangeException(nameof(lifetime)),
        };
}