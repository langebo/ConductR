namespace ConductR;

public interface IEventHandler<TEvent>
{
    ValueTask HandleAsync(TEvent @event, CancellationToken token = default);
}