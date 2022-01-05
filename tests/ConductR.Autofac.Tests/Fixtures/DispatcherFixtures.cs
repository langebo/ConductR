using System.Runtime.CompilerServices;
using Autofac;
using ConductR.Handlers;
using ConductR.Types;

namespace ConductR.Autofac.Tests;

public class DispatcherFixture : IDisposable
{
    public IComponentContext Context { get; }

    public DispatcherFixture()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<CommandHandler>().As<ICommandHandler<Command, Result>>();
        builder.RegisterType<QueryHandler>().As<IQueryHandler<Query, Result>>();
        builder.RegisterType<StreamHandler>().As<IStreamHandler<StreamQuery, Result>>();
        builder.RegisterType<EventHandler>().As<IEventHandler<Event>>();
        Context = builder.Build();
    }

    public void Dispose() => GC.SuppressFinalize(this);
}

public record Command() : ICommand;
public record Query() : IQuery;
public record StreamQuery() : IStreamQuery;
public record Event() : IEvent;
public record Result();

public class CommandHandler : ICommandHandler<Command, Result>
{
    public ValueTask<Result> HandleAsync(Command command, CancellationToken token = default)
        => ValueTask.FromResult(new Result());
}

public class QueryHandler : IQueryHandler<Query, Result>
{
    public ValueTask<Result> HandleAsync(Query query, CancellationToken token = default)
        => ValueTask.FromResult(new Result());
}

public class StreamHandler : IStreamHandler<StreamQuery, Result>
{
    public async IAsyncEnumerable<Result> HandleAsync(StreamQuery query, [EnumeratorCancellation] CancellationToken token = default)
    {
        foreach (var result in Enumerable.Repeat(new Result(), 10))
        {
            await Task.Delay(10, token);
            yield return result;
        }
    }
}

public class EventHandler : IEventHandler<Event>
{
    public ValueTask HandleAsync(Event @event, CancellationToken token = default)
        => ValueTask.CompletedTask;
}