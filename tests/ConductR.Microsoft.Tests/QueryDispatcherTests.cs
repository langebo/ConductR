using ConductR.Microsoft.Dispatchers;
using FluentAssertions;
using Xunit;

namespace ConductR.Microsoft.Tests;

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
        var dispatcher = new QueryDispatcher(fixture.ServiceProvider);
        var result = await dispatcher.DispatchAsync<Query, Result>(new Query(), CancellationToken.None);
        result.Should().NotBeNull();
    }
}