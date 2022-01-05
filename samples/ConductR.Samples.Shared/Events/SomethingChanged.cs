using ConductR.Handlers;
using ConductR.Types;
using Microsoft.Extensions.Logging;

namespace ConductR.Samples.Shared.Events;

public record SomethingChanged(DateTimeOffset Timestamp) : IEvent;

public class ChangedHandler : IEventHandler<SomethingChanged>
{
    private readonly ILogger logger;

    public ChangedHandler(ILogger<ChangedHandler> logger) => this.logger = logger;

    public async ValueTask HandleAsync(SomethingChanged @event, CancellationToken token = default)
    {
        await Task.Delay(50);
        logger.LogInformation("Something changed at {Timestamp:o}", @event.Timestamp);
    }
}