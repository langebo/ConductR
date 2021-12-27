using ConductR.Handlers;
using ConductR.Types;

namespace ConductR.Samples;

public record Ping(string Message) : IQuery;
public record Pong(string Message);

public class PingPongHandler : IQueryHandler<Ping, Pong>
{
    public ValueTask<Pong> HandleAsync(Ping command, CancellationToken token = default)
    {
        return ValueTask.FromResult(new Pong(command.Message));
    }
}