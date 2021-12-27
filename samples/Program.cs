using ConductR;
using ConductR.Samples;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConductR(typeof(Ping));

var app = builder.Build();
app.MapGet("/ping", async (IConductor conductor, string message, CancellationToken token) =>
    await conductor.QueryAsync<Ping, Pong>(new(message), token));

app.MapGet("/sing", (IConductor conductor, int syllables, CancellationToken token) =>
    conductor.StreamAsync<Sing, string>(new(syllables), token));

app.MapPost("/change", async (IConductor conductor, CancellationToken token) =>
{
    var timestamp = await conductor.CommandAsync<ChangeSomething, DateTimeOffset>(new(), token);
    await conductor.PublishAsync(new SomethingChanged(timestamp), token);
    return Results.Ok();
});

await app.RunAsync();
