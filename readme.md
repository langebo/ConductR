# ConductR

ConductR is an opinionated lightweight CQRS-ish mediator library built with dependency injection in mind.

As a heavy user of Jimmy Bogards infamous [MediatR library](https://github.com/jbogard/MediatR), I was dissatisfied with it's lacking support of streaming via `IAsyncEnumerable<T>`. Also I favor the possibility to conform to the CQRS pattern cleanly by separating commands from queries.

So when I read Cezary Piątek's blog post [Why I don't use MediatR for CQRS](https://cezarypiatek.github.io/post/why-i-dont-use-mediatr-for-cqrs/) I wanted to try and build a lightweight CQRS-ish mediator library myself, that doesn't do much magic, but rather utilizes well established patterns and practices like `ìnversion of control`, `decorator pattern` etc.

Since I either use `Microsoft.Extensions.DependencyInjection` or `Autofac` as DI container frameworks, packages for those two are already included via `ConductR.Microsoft` and `ConductR.Autofac`. If you feel like contributing a package for another container framework (or anything else), I will happily review/accept your PR :)

## What's in the box?

Mediator, CQRS and domain use-cases that are currently supported out of the box are:

- **Commands** (`ICommand`, `ICommandHandler<TCommand, TResult>`)

  Mutating (writing) requests, that can be handled asynchronously, returning a result

- **Queries** (`IQuery`, `IQueryHandler<TQuery, TResult>`)

  Non-mutating (read-only) requests, that can be handled asynchronously, returning a result

- **Streams** (`IStreamQuery`, `IStreamHandler<TStreamQuery, TResult`)

  Non-mutating (read-only) requests, that can be handled asynchronously, returning a stream of results

- **Events** (`IEvent`, `IEventHandler<TEvent>`)

  Domain events that can be handled asynchronously

- **Interceptors** (`CommandInterceptor`, `QueryInterceptor`, `StreamInterceptor`, `EventInterceptor`)

  Middleware classes, that can intercept commands, queries, streams (up to individual item level) and events, that can perform various tasks with the input and/or output, forming a pipeline-ish construct. Usual use-cases include input validation, logging, authorization, etc.

## Getting started

### Install the Nuget Package

#### Using Microsoft.Extensions.DependencyInjection

```sh
dotnet add package ConductR.Microsoft
```

#### Using Autofac

```sh
dotnet add package ConductR.Autofac
```

### Add ConductR to the DI container

#### Using Microsoft.Extensions.DependencyInjection

```csharp
builder.Services.AddConductR(typeof(CreateGreetingCommand));
```

#### Using Autofac

```csharp
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    builder.AddConductR(typeof(CreateGreetingCommand));
```

_The type parameter is used to select the assembly containing your Commands, Queries, Handlers, etc._

### Write a Command, Query, StreamQuery or Event and its handler

```csharp
public record CreateGreetingCommand(string Name) : ICommand;

public record Greeting(string Phrase);

public class CreateGreetingHandler : ICommandHandler<CreateGreetingCommand, Greeting>
{
    private readonly IGreetingGenerator greetingGenerator;

    public CreateGreetingHandler(IGreetingGenerator greetingGenerator)
        => this.greetingGenerator = greetingGenerator;

    public async ValueTask<Greeting> HandleAsync(CreateGreetingCommand command,  CancellationToken token = default)
    {
        var phrase = await greetingGenerator.GenerateAsync(command.Name, token);
        return new Greeting(phrase);
    }
}
```

#### Dispatch the command via the `IConductor`

```csharp
app.MapPost("/greeting", async (IConductor conductor, string name, CancellationToken token) =>
    await conductor.CommandAsync<CreateGreetingCommand, Greeting>(new(name), token));
```

## Further features

Features like building a pipeline via interceptors, configuring the DI service lifetime, etc. can be found in the `samples` area of the repository.
