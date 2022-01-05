using ConductR.Autofac.Dispatchers;
using FluentAssertions;
using Xunit;

namespace ConductR.Autofac.Tests;

public class EventDispatcherTests : IClassFixture<DispatcherFixture>
{
    private readonly DispatcherFixture fixture;

    public EventDispatcherTests(DispatcherFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task DispatchAsync_RegularEvent_DoesNotThrow()
    {
        var dispatcher = new EventDispatcher(fixture.Context);
        Func<Task> act = async () => await dispatcher.DispatchAsync<Event>(new(), CancellationToken.None).AsTask();
        await act.Should().NotThrowAsync();
    }
}