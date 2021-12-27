using ConductR.Types;

namespace ConductR.Handlers;

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand
{
    ValueTask<TResult> HandleAsync(TCommand command, CancellationToken token = default);
}