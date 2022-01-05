using System.Runtime.CompilerServices;
using ConductR.Handlers;
using ConductR.Types;
using Microsoft.Extensions.DependencyInjection;

namespace ConductR.Microsoft.Tests;

public class DispatcherFixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; }

    public DispatcherFixture()
    {
        ServiceProvider = new ServiceCollection()
            .AddTransient<ICommandHandler<Command, Result>, CommandHandler>()
            .AddTransient<IQueryHandler<Query, Result>, QueryHandler>()
            .AddTransient<IStreamHandler<StreamQuery, Result>, StreamHandler>()
            .AddTransient<IEventHandler<Event>, EventHandler>()
            .BuildServiceProvider();
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
