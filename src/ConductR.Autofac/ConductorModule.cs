using System.Reflection;
using Autofac;
using ConductR.Autofac.Dispatchers;
using ConductR.Dispatchers;
using ConductR.Handlers;
using Module = Autofac.Module;

namespace ConductR.Autofac;

public class ConductorModule : Module
{
    private readonly AutofacLifetime lifetime;
    private readonly Assembly[] assemblies;

    public ConductorModule(AutofacLifetime lifetime, Assembly[] assemblies)
    {
        this.lifetime = lifetime;
        this.assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
        if (assemblies.Length <= 0) throw new ArgumentException("Assemblies must not be empty", nameof(assemblies));
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(ICommandHandler<,>))
            .InstancePer(lifetime);

        builder.RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(IQueryHandler<,>))
            .InstancePer(lifetime);

        builder.RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(IStreamHandler<,>))
            .InstancePer(lifetime);

        builder.RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(IEventHandler<>))
            .InstancePer(lifetime);

        builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePer(lifetime);
        builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>().InstancePer(lifetime);
        builder.RegisterType<StreamDispatcher>().As<IStreamDispatcher>().InstancePer(lifetime);
        builder.RegisterType<EventDispatcher>().As<IEventDispatcher>().InstancePer(lifetime);
        builder.RegisterType<Conductor>().As<IConductor>().InstancePer(lifetime);
    }
}
