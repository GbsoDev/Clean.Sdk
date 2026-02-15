# Quick Reference

A compact operational guide for daily development with `Clean.Sdk`.

Evidence date: **2026-02-18**.

---

## Core Commands

```bash
# Restore dependencies
dotnet restore

# Build all projects
dotnet build

# Run all tests
dotnet test

# Run one test project
dotnet test Clean.Sdk.Domain.Tests/Clean.Sdk.Domain.Tests.csproj
```

---

## Build Scripts

```powershell
# Windows
./build.ps1 --config Beta
./build.ps1 --config Release
```

```bash
# Linux / macOS
./build.sh --config Beta
./build.sh --config Release
```

Build order used by scripts:

1. `Clean.Sdk.Domain`
2. `Clean.Sdk.Application`
3. `Clean.Sdk.Data.EfCore`
4. `Clean.Sdk.Infrastructure`

---

## Layer Responsibilities (Fast View)

- `Domain`: business contracts, services, validations, exceptions
- `Application`: commands/queries, handlers, mapping orchestration
- `Data.EfCore`: EF repository implementation and persistence concerns
- `Infrastructure`: DI registration, providers, setup extensions

---

## Registration Attributes

| Attribute | Scanned by |
|---|---|
| `[Service]` | `AddDomainServices(assembly)` |
| `[Repository]` | `AddRepositories(assembly)` |
| `[AplicacionHandler]` | marker only — use `AddMediatR(assembly)` |
| `[MapperProfile]` | `AddAutoMapperProfiles(assembly)` |
| `[Option]` | `ConfigureAppSettingOptions<T>(...)` |

---

## Common DI Setup

```csharp
// Domain services, repositories, MediatR handlers, AutoMapper
services
    .AddDomainServices(assembly)
    .AddRepositories(assembly)
    .AddMediatR(assembly)              // registers [AplicacionHandler] handlers
    .AddAutoMapperProfiles(assembly);  // registers [MapperProfile] profiles

// EF Core context (two type params: interface + implementation)
services.AddEfCoreContext<IMyDbContext, MyDbContext>(dbConnection);

// Configuration options
services.ConfigureAppSettingOptions<AppSettings>(ref configuration, out var appSettings);

// JWT + CORS
services
    .AddWebApiAutenticacionToken(appSettings)
    .AddWebApiCorsPolicies(appSettings);

// Lazy<T> resolution
services.AddLazySupport();
```

---

## Validation Snippets

```csharp
ArgumentValidationExtensions.ThrowIfNull(value, nameof(value));
ArgumentValidationExtensions.ThrowIfNullOrEmpty(text, nameof(text));
```

```csharp
var validations = new ValidationSet("Validation failed");
if (string.IsNullOrWhiteSpace(name)) validations.AddError("Name is required");
validations.ValidateAndThrow();
```

---

## Handler Skeleton

```csharp
[AplicacionHandler]
public class CreateEntityHandler : SaveHandler<CreateEntityCommand, EntityDto, Entity, IEntityService>
{
    public CreateEntityHandler(ILogger<Handler> logger, IMapper mapper, Lazy<IEntityService> service)
        : base(logger, mapper, service)
    {
    }

    protected override AbstractValidator<CreateEntityCommand>? ValidationRules => new CreateEntityValidator();
}
```

---

## Domain Service Skeleton

Use specialized base classes per operation. `CrudService` is **obsolete**.

```csharp
// Single-operation service (save only)
[Service]
public class EntitySaveService
    : SaveService<Entity, IEntityRepository>, IEntitySaveService
{
    public EntitySaveService(ILogger<Service> logger, Lazy<IEntityRepository> repository)
        : base(logger, repository) { }
}

// Multi-operation service (extend ActionService and implement interfaces manually)
[Service]
public class EntityService
    : ActionService<Entity, IEntityRepository>, IEntityService
{
    public EntityService(ILogger<Service> logger, Lazy<IEntityRepository> repository)
        : base(logger, repository) { }

    // implement ISaveService<T>, IUpdateService<T>, IDeleteService<T> as needed
}
```

---

## Exception Shortlist

- `NotFoundException`
- `NullException`
- `NullOrEmptyException`
- `InvalidArgumentException`
- `ValidationException`
- `ValidationSetException`
- `NotAuthorizeException`

---

## Naming Checklist

- Interfaces: `IName`
- Async methods: `NameAsync`
- DTOs: `NameDto`
- Commands: `VerbEntityCommand`
- Queries: `GetEntityQuery`
- Handlers: `RequestNameHandler`

---

## New Feature Checklist

- [ ] Domain service/interface created
- [ ] Command/query + handler created
- [ ] Mapper profile added
- [ ] Repository behavior added/updated if needed
- [ ] DI registration works via marker attributes
- [ ] Tests added/updated in corresponding test project
- [ ] `docs/` updated for externally visible behavior changes
