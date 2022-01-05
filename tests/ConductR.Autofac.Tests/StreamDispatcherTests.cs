using ConductR.Autofac.Dispatchers;
using FluentAssertions;
using Xunit;

namespace ConductR.Autofac.Tests;

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
        var dispatcher = new StreamDispatcher(fixture.Context);
        var result = await dispatcher.DispatchAsync<StreamQuery, Result>(new(), CancellationToken.None)
            .ToListAsync(CancellationToken.None);

        result.Should().NotBeNull()
            .And.NotBeEmpty()
            .And.HaveCount(10);
    }
}