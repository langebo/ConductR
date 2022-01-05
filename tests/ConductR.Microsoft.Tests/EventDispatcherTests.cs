using ConductR.Microsoft.Dispatchers;
using FluentAssertions;
using Xunit;

namespace ConductR.Microsoft.Tests;

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
        var dispatcher = new EventDispatcher(fixture.ServiceProvider);
        Func<Task> act = async () => await dispatcher.DispatchAsync(new Event(), CancellationToken.None).AsTask();
        await act.Should().NotThrowAsync();
    }
}