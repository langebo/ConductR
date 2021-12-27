using ConductR.Handlers;
using ConductR.Types;

namespace ConductR.Samples;

public record ChangeSomething() : ICommand;
public record SomethingChanged(DateTimeOffset Timestamp) : IEvent;

public class ChangeHandler : ICommandHandler<ChangeSomething, DateTimeOffset>
{
    public ValueTask<DateTimeOffset> HandleAsync(ChangeSomething command, CancellationToken token = default)
    {
        return ValueTask.FromResult(DateTimeOffset.Now);
    }
}

public class ChangedHandler : IEventHandler<SomethingChanged>
{
    private readonly ILogger logger;

    public ChangedHandler(ILogger<ChangedHandler> logger)
    {
        this.logger = logger;
    }

    public ValueTask HandleAsync(SomethingChanged @event, CancellationToken token = default)
    {
        logger.LogInformation("Something changed at {Timestamp:g}", @event.Timestamp);
        return ValueTask.CompletedTask;
    }
}
