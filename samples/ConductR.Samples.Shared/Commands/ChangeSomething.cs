using System;
using ConductR.Handlers;
using ConductR.Types;

namespace ConductR.Samples.Shared.Commands;

public record ChangeSomething() : ICommand;

public class ChangeHandler : ICommandHandler<ChangeSomething, DateTimeOffset>
{
    public async ValueTask<DateTimeOffset> HandleAsync(ChangeSomething command, CancellationToken token = default)
    {
        await Task.Delay(50);
        return DateTimeOffset.Now;
    }
}