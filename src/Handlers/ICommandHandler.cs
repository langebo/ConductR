namespace ConductR;
public interface ICommandHandler<in TCommand, TResult>
{
    ValueTask<TResult> HandleAsync(TCommand command, CancellationToken token = default);
}