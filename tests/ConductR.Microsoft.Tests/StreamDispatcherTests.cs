using ConductR.Microsoft.Dispatchers;
using FluentAssertions;
using Xunit;

namespace ConductR.Microsoft.Tests;

public class StreamDispatcherTests : IClassFixture<DispatcherFixture>
{
    private readonly DispatcherFixture fixture;

    public StreamDispatcherTests(DispatcherFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task DispatchAsync_RegularStreamQuery_ReturnsResults()
    {
        var dispatcher = new StreamDispatcher(fixture.ServiceProvider);
        var result = await dispatcher.DispatchAsync<StreamQuery, Result>(new StreamQuery(), CancellationToken.None)
            .ToListAsync(CancellationToken.None);

        result.Should().NotBeNull()
            .And.NotBeEmpty()
            .And.HaveCount(10);
    }
}