using ConductR.Microsoft.Dispatchers;
using FluentAssertions;
using Xunit;

namespace ConductR.Microsoft.Tests;

public class CommandDispatcherTests : IClassFixture<DispatcherFixture>
{
    private readonly DispatcherFixture fixture;

    public CommandDispatcherTests(DispatcherFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task DispatchAsync_RegularCommand_ReturnsResult()
    {
        var dispatcher = new CommandDispatcher(fixture.ServiceProvider);
        var result = await dispatcher.DispatchAsync<Command, Result>(new(), CancellationToken.None);
        result.Should().NotBeNull();
    }
}