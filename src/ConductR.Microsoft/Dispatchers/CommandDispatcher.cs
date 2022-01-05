using ConductR.Dispatchers;
using ConductR.Handlers;
using ConductR.Types;
using Microsoft.Extensions.DependencyInjection;

namespace ConductR.Microsoft.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider) => this.serviceProvider = serviceProvider;

    public ValueTask<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken token = default) where TCommand : ICommand
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return handler.HandleAsync(command, token);
    }
}