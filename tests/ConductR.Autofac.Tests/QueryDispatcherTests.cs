using ConductR.Autofac.Dispatchers;
using FluentAssertions;
using Xunit;

namespace ConductR.Autofac.Tests;

public class QueryDispatcherTests : IClassFixture<DispatcherFixture>
{
    private readonly DispatcherFixture fixture;

    public QueryDispatcherTests(DispatcherFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task DispatchAsync_RegularQuery_ReturnsResult()
    {
        var dispatcher = new QueryDispatcher(fixture.Context);
        var result = await dispatcher.DispatchAsync<Query, Result>(new(), CancellationToken.None);
        result.Should().NotBeNull();
    }
}