# DotNet-DDD-Template

A production-ready N-Tier .NET solution template implementing Domain-Driven Design (DDD), CQRS with MediatR, EF Core, and Azure-ready infrastructure.

## Architecture

```
src/
  DotNet.DDD.Template.Domain/          ← Aggregates, Value Objects, Domain Events, Repository interfaces
  DotNet.DDD.Template.Application/     ← CQRS Commands/Queries, MediatR Handlers, App Services
  DotNet.DDD.Template.Infrastructure/  ← EF Core, Azure Service Bus, Redis, HTTP clients
  DotNet.DDD.Template.WebApi/          ← ASP.NET Core Controllers, Middleware, DI wiring
```

## Dependency Rule

Outer tiers depend inward. The Domain tier has **zero** external dependencies — pure C#.

```
WebApi → Application → Domain ← Infrastructure
```

## Tech Stack

| Layer | Technology |
|---|---|
| API | ASP.NET Core 8 Minimal APIs + Controllers |
| CQRS | MediatR 12 |
| Validation | FluentValidation |
| Mapping | AutoMapper |
| ORM | Entity Framework Core 8 |
| Messaging | Azure Service Bus |
| Caching | Redis (StackExchange.Redis) |
| Auth | ASP.NET Core Identity + JWT Bearer |

## Getting Started

```bash
git clone https://github.com/nilendrat-ai/DotNet-DDD-Template.git
cd DotNet-DDD-Template
dotnet restore
dotnet build
dotnet run --project src/DotNet.DDD.Template.WebApi
```

## Project Structure

See each project's README for layer-specific documentation.
