using ConductR.Dispatchers;
using ConductR.Types;

namespace ConductR.Interceptors;

public abstract class CommandInterceptor : ICommandDispatcher
{
    protected virtual ICommandDispatcher Dispatcher { get; }
    protected CommandInterceptor(ICommandDispatcher dispatcher) => Dispatcher = dispatcher;

    public abstract ValueTask<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken token = default) where TCommand : ICommand;
}