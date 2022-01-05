using ConductR.Dispatchers;
using ConductR.Tests.Fixtures;
using FluentAssertions;
using Moq;
using Xunit;

namespace ConductR.Tests;

public class ConductorTests : IClassFixture<ConductorFixture>
{
    private readonly ConductorFixture fixture;

    public ConductorTests(ConductorFixture fixture) => this.fixture = fixture;

    [Fact]
    public void Constructor_NullCommandDispatcher_ThrowsArgumentNullException()
    {
        ICommandDispatcher? commandDispatcher = null;

#pragma warning disable CS8604, CA1806
        Action act = () => new Conductor(
            commandDispatcher,
            fixture.QueryDispatcherMock.Object,
            fixture.StreamDispatcherMock.Object,
            fixture.EventDispatcherMock.Object);
#pragma warning restore CS8604, CA1806

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(commandDispatcher));
    }

    [Fact]
    public void Constructor_NullQueryDispatcher_ThrowsArgumentNullException()
    {
        IQueryDispatcher? queryDispatcher = null;

#pragma warning disable CS8604, CA1806
        Action act = () => new Conductor(
            fixture.CommandDispatcherMock.Object,
            queryDispatcher,
            fixture.StreamDispatcherMock.Object,
            fixture.EventDispatcherMock.Object);
#pragma warning disable CS8604, CA1806

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(queryDispatcher));
    }

    [Fact]
    public void Constructor_NullStreamDispatcher_ThrowsArgumentNullException()
    {
        IStreamDispatcher? streamDispatcher = null;

#pragma warning disable CS8604, CA1806
        Action act = () => new Conductor(
            fixture.CommandDispatcherMock.Object,
            fixture.QueryDispatcherMock.Object,
            streamDispatcher,
            fixture.EventDispatcherMock.Object);
#pragma warning disable CS8604, CA1806

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(streamDispatcher));
    }

    [Fact]
    public void Constructor_NullEventDispatcher_ThrowsArgumentNullException()
    {
        IEventDispatcher? eventDispatcher = null;

#pragma warning disable CS8604, CA1806
        Action act = () => new Conductor(
            fixture.CommandDispatcherMock.Object,
            fixture.QueryDispatcherMock.Object,
            fixture.StreamDispatcherMock.Object,
            eventDispatcher);
#pragma warning disable CS8604, CA1806

        act.Should().Throw<ArgumentNullException>()
            .WithParameterName(nameof(eventDispatcher));
    }

    [Fact]
    public async Task CommandAsync_RegularCommand_ShouldDispatch()
    {
        var result = new ConductorFixture.Result();
        fixture.CommandDispatcherMock
            .Setup(x => x.DispatchAsync<ConductorFixture.Command, ConductorFixture.Result>(
                It.IsAny<ConductorFixture.Command>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        var commandResult = await fixture.Conductor.CommandAsync<ConductorFixture.Command, ConductorFixture.Result>(new(), CancellationToken.None);

        commandResult.Should().NotBeNull();
        commandResult.Should().BeSameAs(result);
        fixture.CommandDispatcherMock.Verify(x => x.DispatchAsync<ConductorFixture.Command, ConductorFixture.Result>(
                It.IsAny<ConductorFixture.Command>(),
                It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task QueryAsync_RegularQuery_ShouldDispatch()
    {
        var result = new ConductorFixture.Result();
        fixture.QueryDispatcherMock
            .Setup(x => x.DispatchAsync<ConductorFixture.Query, ConductorFixture.Result>(
                It.IsAny<ConductorFixture.Query>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        var queryResult = await fixture.Conductor.QueryAsync<ConductorFixture.Query, ConductorFixture.Result>(new(), CancellationToken.None);

        queryResult.Should().NotBeNull();
        queryResult.Should().BeSameAs(result);
        fixture.QueryDispatcherMock.Verify(x => x.DispatchAsync<ConductorFixture.Query, ConductorFixture.Result>(
                It.IsAny<ConductorFixture.Query>(),
                It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task StreamQueryAsync_RegularStreamQuery_ShouldDispatch()
    {
        var result = AsyncEnumerable.Empty<ConductorFixture.Result>;
        fixture.StreamDispatcherMock
            .Setup(x => x.DispatchAsync<ConductorFixture.StreamQuery, ConductorFixture.Result>(
                It.IsAny<ConductorFixture.StreamQuery>(),
                It.IsAny<CancellationToken>()))
            .Returns(result);

        var streamResult = await fixture.Conductor.StreamAsync<ConductorFixture.StreamQuery, ConductorFixture.Result>(new(), CancellationToken.None)
            .ToListAsync(CancellationToken.None);

        streamResult.Should().NotBeNull();
        streamResult.Should().BeEmpty();
        fixture.StreamDispatcherMock.Verify(x => x.DispatchAsync<ConductorFixture.StreamQuery, ConductorFixture.Result>(
                It.IsAny<ConductorFixture.StreamQuery>(),
                It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task PublishAsync_RegularEvent_ShouldDispatch()
    {
        await fixture.Conductor.PublishAsync<ConductorFixture.Event>(new(), CancellationToken.None);

        fixture.EventDispatcherMock.Verify(x => x.DispatchAsync(
            It.IsAny<ConductorFixture.Event>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}