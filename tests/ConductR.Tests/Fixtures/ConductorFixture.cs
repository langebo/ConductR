using ConductR.Dispatchers;
using ConductR.Types;
using Moq;

namespace ConductR.Tests.Fixtures;

public class ConductorFixture : IDisposable
{
    public record Command() : ICommand;
    public record Query() : IQuery;
    public record StreamQuery() : IStreamQuery;
    public record Event() : IEvent;
    public record Result();

    public Mock<ICommandDispatcher> CommandDispatcherMock { get; set; }
    public Mock<IQueryDispatcher> QueryDispatcherMock { get; set; }
    public Mock<IStreamDispatcher> StreamDispatcherMock { get; set; }
    public Mock<IEventDispatcher> EventDispatcherMock { get; set; }
    public Conductor Conductor { get; set; }

    public ConductorFixture()
    {
        CommandDispatcherMock = new Mock<ICommandDispatcher>();
        QueryDispatcherMock = new Mock<IQueryDispatcher>();
        StreamDispatcherMock = new Mock<IStreamDispatcher>();
        EventDispatcherMock = new Mock<IEventDispatcher>();
        Conductor = new Conductor(
            CommandDispatcherMock.Object,
            QueryDispatcherMock.Object,
            StreamDispatcherMock.Object,
            EventDispatcherMock.Object);
    }

    public void Dispose() => GC.SuppressFinalize(this);
}
