# Contributing to Clean.Sdk

This guide explains how to contribute to `Clean.Sdk` by implementing new features or fixing bugs while adhering to the existing architecture and conventions.

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

Follow these steps to implement a new feature:

1. Define the business behavior in `Domain` first.
2. Expose the use case through `Application` (command/query + handler).
3. Implement data concerns in `Data.EfCore` only when needed.
4. Register and wire dependencies through `Infrastructure` extensions.
5. Add or update tests in the corresponding `*.Tests` project.

### Add a New Domain Service

#### Service Base Classes

Choose the base class that matches the operations your service needs to expose:

| Base class | Interface provided | Operations |
|---|---|---|
| `SaveService<TModel, TRepo>` | `ISaveService<TModel>` | `SaveAsync` |
| `UpdateService<TModel, TRepo>` | `IUpdateService<TModel>` | `UpdateAsync` |
| `DeleteService<TModel, TRepo>` | `IDeleteService<TModel>` | `DeleteByIdAsync`, `DeleteAsync` |
| `ActionService<TModel, TRepo>` | *(none — exposes `Repository`)* | Custom |

> **Note:** `CrudService` is currently marked `[Obsolete]` and must not be used. Compose the individual service bases above instead.

#### 1) Create Service Interface

```csharp
public interface ICustomerService
    : ISaveService<Customer>, IUpdateService<Customer>, IDeleteService<Customer>
{
    Task<Customer?> GetActiveByIdAsync(object id, CancellationToken cancellationToken = default);
}
```

#### 2) Create Service Implementation

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

#### 3) Add Business Validation

Use `ValidationSet` to aggregate rule failures before throwing:

```csharp
var validations = new ValidationSet("Customer validation failed");
if (string.IsNullOrWhiteSpace(customer.Name))
{
    validations.AddError("Name is required.");
}
validations.ValidateAndThrow();
```

### Add a New CQRS Handler

#### 1) Define Command or Query

```csharp
public record CreateCustomerCommand(string Name, string Email) : IRequest<CustomerDto>;
```

#### 2) Implement Handler

```csharp
[AplicacionHandler]  // Note: attribute name uses Spanish spelling; planned normalization
public class CreateCustomerHandler
    : SaveHandler<CreateCustomerCommand, CustomerDto, Customer, ICustomerService>
{
    public CreateCustomerHandler(
        Lazy<ILoggerService> logger,
        IMapper mapper,
        Lazy<ICustomerService> service)
        : base(logger, mapper, service)
    {
    }

    protected override AbstractValidator<CreateCustomerCommand>? ValidationRules => new CreateCustomerCommandValidator();
}
```

> **Note on Logger:** Handlers utilize a decoupled `ILoggerService` port via `Lazy<T>` injection, improving architectural purity and testability.

#### 3) Add Validator

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

### Add Mapper Profile

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

### Add Repository Implementation (EF Core)

Implement repository classes in `Clean.Sdk.Data.EfCore` by extending existing repository abstractions and reusing `EfRepository<...>` where possible.

Practical recommendations:

- Keep query logic in repository layer only
- Keep business rules in domain services
- Use cancellation tokens in async methods
- Keep mapping deterministic and testable

### Dependency Registration

In the consumer application, register layers through extension methods:

```csharp
// Register domain services, repositories, MediatR handlers, and AutoMapper profiles
services
    .AddDomainServices(assembly)
    .AddRepositories(assembly)
    .AddMediatR(assembly)                           // registers all [AplicacionHandler] handlers by scanning assembly
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

## Coding Conventions

These conventions keep `Clean.Sdk` consistent and predictable across teams and features.

### Naming Conventions

- Use `PascalCase` for classes, records, interfaces (with `I` prefix), methods, and public properties.
- Use `camelCase` for parameters and local variables.
- Use `_camelCase` for private fields.
- Use async suffix `Async` for asynchronous methods.

#### Known Naming Inconsistencies

The following identifiers contain typos or inconsistencies and are scheduled for normalization in a future major version. **Do not introduce new naming inconsistencies; use the correct spelling in new code.**

| Current | Should Be | Location |
|---------|-----------|----------|
| `AppExeption` | `AppException` | `Domain/Exceptions/AppExeption.cs` |
| `TServie` | `TService` | `Application/Handlers/*.cs` |
| `AplicacionHandlerAttribute` | `ApplicationHandlerAttribute` | `Application/Handlers/AplicacionHandlerAttribute.cs` |
| `GeyTypesByAttribute` | `GetTypesByAttribute` | `Domain/Helpers/AssemblyHelper.cs` |
| `SecctionName` | `SectionName` | `Infrastructure/Extensions/OptionsProvider.cs` |
| `dbConecction` | `dbConnection` | `Infrastructure/Extensions/EfCoreProvider.cs` |

#### Layer-Specific

- **Domain:**
  - Service interfaces: `I{Name}Service`
  - Service implementations: `{Name}Service`
  - Domain exceptions: `{Name}Exception`
  - Validators: `{Name}Validator`
- **Application:**
  - Commands: `{Verb}{Entity}Command`
  - Queries: `Get{Entity}Query`, `Get{Entity}ByIdQuery`
  - Handlers: `{RequestName}Handler`
  - DTOs: `{Entity}Dto`
  - Profiles: `{Entity}Profile`
- **Data:**
  - Repository interfaces: `I{Entity}Repository`
  - Repository implementations: `{Entity}Repository`
  - Context classes: `{Name}DbContext`

### Marker Attributes

Use marker attributes for automatic registration:

| Attribute | Purpose | Scanned By |
|-----------|---------|------------|
| `[Service]` | Domain services | `AddDomainServices(assembly)` |
| `[Repository]` | Repositories | `AddRepositories(assembly)` |
| `[AplicacionHandler]` | Application handlers | `AddMediatR(assembly)` |
| `[MapperProfile]` | AutoMapper profiles | `AddAutoMapperProfiles(assembly)` |
| `[Option]` | Configuration option classes | `ConfigureAppSettingOptions<T>(...)` |

> **Note:** The attribute name `AplicacionHandler` uses Spanish spelling. Normalization to `ApplicationHandler` is planned for a future major version.

### Folder Organization

Follow existing project boundaries:

- Domain concerns in `Clean.Sdk.Domain`
- Use-case orchestration in `Clean.Sdk.Application`
- Persistence in `Clean.Sdk.Data.EfCore`
- Composition and setup in `Clean.Sdk.Infrastructure`

Within each project, keep files grouped by feature or concern (services, handlers, validations, options, etc.).

### Class Layout (Recommended)

1. Constants
2. Static fields
3. Instance fields
4. Properties
5. Constructors
6. Public methods
7. Protected methods
8. Private methods

Keep constructor injection explicit and avoid hidden service resolution.

### Async and Cancellation

- Prefer `Task`/`Task<T>` for I/O-bound operations.
- Pass `CancellationToken` through service, handler, and repository layers.
- Avoid blocking calls (`.Result`, `.Wait()`) in asynchronous paths.

### Validation and Exceptions

- Validate null/empty values at boundaries.
- Use `ValidationSet` for aggregated validation failures.
- Throw domain-specific exceptions over generic `Exception`.
- Keep validation messages clear and actionable.

### Logging

- Use constructor-injected `ILoggerService` through base classes and concrete implementations.
- Log decision points and failures, not trivial flow noise.
- Avoid logging sensitive data.

---

## Testing Guidance

Map tests to architectural responsibility:

- `Clean.Sdk.Domain.Tests`: business behavior, validation, exceptions
- `Clean.Sdk.Application.Tests`: handler orchestration and mapping behavior
- `Clean.Sdk.Data.EfCore.Tests`: repository persistence logic
- `Clean.Sdk.Infrastructure.Tests`: DI registration and extension correctness

### Test Conventions

- Test class: `{ClassUnderTest}Tests`
- Test method: `Method_WhenCondition_ShouldResult`
- Arrange/Act/Assert structure is preferred.
- Keep unit tests deterministic and independent.
- Use the recommendation: `MethodName_WhenCondition_ShouldExpectedResult`.

### Known Limitations

- Tests are primarily mock-based; integration tests with real databases are limited.
- Consider adding integration tests for critical repository operations.

---

## Pull Request Checklist

- [ ] Feature follows layer boundaries
- [ ] New classes follow naming conventions
- [ ] DI auto-registration works as expected
- [ ] Unit tests added/updated
- [ ] Existing tests pass for impacted projects
- [ ] Documentation in `docs/` updated when behavior changes

---

## Do / Don’t

### Do

- Respect layer ownership of responsibilities
- Reuse base classes where intended
- Keep mapping and validation explicit
- Document changes that alter public behavior

### Don’t

- Put business rules in repository classes
- Bypass handlers for application use cases
- Introduce hard-coded dependencies
- Hide side effects in mapping or validators
