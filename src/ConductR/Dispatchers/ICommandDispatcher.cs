using ConductR.Types;

namespace ConductR.Dispatchers;

public interface ICommandDispatcher
{
    ValueTask<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken token = default) where TCommand : ICommand;
}