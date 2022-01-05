using Autofac;
using ConductR.Dispatchers;
using ConductR.Handlers;
using ConductR.Types;

namespace ConductR.Autofac.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IComponentContext context;

    public CommandDispatcher(IComponentContext context) => this.context = context;

    public ValueTask<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken token = default) where TCommand : ICommand
    {
        var handler = context.Resolve<ICommandHandler<TCommand, TResult>>();
        return handler.HandleAsync(command, token);
    }
}