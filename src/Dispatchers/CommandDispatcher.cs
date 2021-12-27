using Microsoft.Extensions.DependencyInjection;

namespace ConductR;
public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public ValueTask<TResult> DispatchAsync<TCommand, TResult>(TCommand query, CancellationToken token = default)
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return handler.HandleAsync(query, token);
    }
}