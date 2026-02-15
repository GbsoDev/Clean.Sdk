# Development Guide

## Goal

This guide explains how to implement new features in `Clean.Sdk` using the existing architecture and extension points.

Documentation baseline reference: [DOCUMENTATION_STANDARD.md](DOCUMENTATION_STANDARD.md).

---

## Prerequisites

- .NET SDK 8.x
- A clean restore/build locally
- Familiarity with CQRS and dependency injection

Core commands:

```bash
dotnet restore
dotnet build
dotnet test
```

---

## Development Workflow

1. Define the business behavior in `Domain` first.
2. Expose the use case through `Application` (command/query + handler).
3. Implement data concerns in `Data.EfCore` only when needed.
4. Register and wire dependencies through `Infrastructure` extensions.
5. Add or update tests in the corresponding `*.Tests` project.

---

## Add a New Domain Service

### Service Base Classes

Choose the base class that matches the operations your service needs to expose:

| Base class | Interface provided | Operations |
|---|---|---|
| `SaveService<TModel, TRepo>` | `ISaveService<TModel>` | `SaveAsync` |
| `UpdateService<TModel, TRepo>` | `IUpdateService<TModel>` | `UpdateAsync` |
| `DeleteService<TModel, TRepo>` | `IDeleteService<TModel>` | `DeleteByIdAsync`, `DeleteAsync` |
| `ActionService<TModel, TRepo>` | *(none — exposes `Repository`)* | Custom |

> **Note:** `CrudService` is currently marked `[Obsolete]` and must not be used. Compose the individual service bases above instead.

### 1) Create Service Interface

```csharp
public interface ICustomerService
    : ISaveService<Customer>, IUpdateService<Customer>, IDeleteService<Customer>
{
    Task<Customer?> GetActiveByIdAsync(object id, CancellationToken cancellationToken = default);
}
```

### 2) Create Service Implementation

For single-operation services, extend the matching base class directly:

```csharp
// Save-only service example
[Service]
public class CustomerSaveService : SaveService<Customer, ICustomerRepository>, ICustomerSaveService
{
    public CustomerSaveService(ILogger<Service> logger, Lazy<ICustomerRepository> repository)
        : base(logger, repository)
    {
    }
}
```

For multi-operation services, extend `ActionService` and implement each interface explicitly:

```csharp
[Service]
public class CustomerService : ActionService<Customer, ICustomerRepository>, ICustomerService
{
    public CustomerService(ILogger<Service> logger, Lazy<ICustomerRepository> repository)
        : base(logger, repository)
    {
    }

    public async Task<Customer> SaveAsync(Customer entity, CancellationToken cancellationToken)
    {
        var result = await Repository.SaveAsync(entity, cancellationToken);
        await Repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<Customer> UpdateAsync(Customer entity, CancellationToken cancellationToken)
    {
        var result = await Repository.UpdateAsync(entity, cancellationToken);
        await Repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken)
    {
        var deleted = await Repository.DeleteByIdAsync(id, cancellationToken);
        if (deleted) await Repository.SaveChangesAsync(cancellationToken);
        return deleted;
    }

    public async Task<bool> DeleteAsync(Customer entity, CancellationToken cancellationToken)
    {
        var deleted = await Repository.DeleteAsync(entity, cancellationToken);
        if (deleted) await Repository.SaveChangesAsync(cancellationToken);
        return deleted;
    }

    public async Task<Customer?> GetActiveByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));
        return await Repository.GetByIdAsync(id, cancellationToken);
    }
}
```

### 3) Add Business Validation

Use `ValidationSet` to aggregate rule failures before throwing:

```csharp
var validations = new ValidationSet("Customer validation failed");
if (string.IsNullOrWhiteSpace(customer.Name))
{
    validations.AddError("Name is required.");
}
validations.ValidateAndThrow();
```

---

## Add a New CQRS Handler

### 1) Define Command or Query

```csharp
public record CreateCustomerCommand(string Name, string Email) : IRequest<CustomerDto>;
```

### 2) Implement Handler

```csharp
[AplicacionHandler]
public class CreateCustomerHandler
    : SaveHandler<CreateCustomerCommand, CustomerDto, Customer, ICustomerService>
{
    public CreateCustomerHandler(
        ILogger<Handler> logger,
        IMapper mapper,
        Lazy<ICustomerService> service)
        : base(logger, mapper, service)
    {
    }

    protected override AbstractValidator<CreateCustomerCommand>? ValidationRules => new CreateCustomerCommandValidator();
}
```

### 3) Add Validator

```csharp
public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleSet(ValidationsSet.SAVE, () =>
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        });
    }
}
```

---

## Add Mapper Profile

```csharp
[MapperProfile]
public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<Customer, CustomerDto>();
    }
}
```

---

## Add Repository Implementation (EF Core)

Implement repository classes in `Clean.Sdk.Data.EfCore` by extending existing repository abstractions and reusing `EfRepository<...>` where possible.

Practical recommendations:

- Keep query logic in repository layer only
- Keep business rules in domain services
- Use cancellation tokens in async methods
- Keep mapping deterministic and testable

---

## Dependency Registration

In the consumer application, register layers through extension methods:

```csharp
// Register domain services, repositories, MediatR handlers, and AutoMapper profiles
services
    .AddDomainServices(assembly)
    .AddRepositories(assembly)
    .AddMediatR(assembly)                           // registers all [AplicacionHandler] handlers
    .AddAutoMapperProfiles(assembly);               // registers all [MapperProfile] profiles

// Register EF Core context (two type params: interface + implementation)
services.AddEfCoreContext<IMyDbContext, MyDbContext>(dbConnection);

// Register configuration options
services.ConfigureAppSettingOptions<AppSettings>(ref configuration, out var appSettings);

// Register JWT authentication and CORS policies (requires AppSettings)
services
    .AddWebApiAutenticacionToken(appSettings)
    .AddWebApiCorsPolicies(appSettings);

// Register Lazy<T> support
services.AddLazySupport();
```

---

## Testing Guidance

Map tests to architectural responsibility:

- `Clean.Sdk.Domain.Tests`: business behavior, validation, exceptions
- `Clean.Sdk.Application.Tests`: handler orchestration and mapping behavior
- `Clean.Sdk.Data.EfCore.Tests`: repository persistence logic
- `Clean.Sdk.Infrastructure.Tests`: DI registration and extension correctness

Test naming recommendation:

`MethodName_WhenCondition_ShouldExpectedResult`

---

## Pull Request Checklist

- [ ] Feature follows layer boundaries
- [ ] New classes follow naming conventions
- [ ] DI auto-registration works as expected
- [ ] Unit tests added/updated
- [ ] Existing tests pass for impacted projects
- [ ] Documentation in `docs/` updated when behavior changes
- [ ] Architecture and metrics docs updated when dependencies, build flow, or quality indicators change
