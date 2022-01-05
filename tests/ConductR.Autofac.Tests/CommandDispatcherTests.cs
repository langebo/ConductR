using ConductR.Autofac.Dispatchers;
using FluentAssertions;
using Xunit;

namespace ConductR.Autofac.Tests;

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
        var dispatcher = new CommandDispatcher(fixture.Context);
        var result = await dispatcher.DispatchAsync<Command, Result>(new Command(), CancellationToken.None);
        result.Should().NotBeNull();
    }
}