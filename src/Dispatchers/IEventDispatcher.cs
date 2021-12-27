namespace ConductR;

public interface IEventDispatcher
{
    ValueTask DispatchAsync<TEvent>(TEvent @event, CancellationToken token = default);
}