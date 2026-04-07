# Conventions

## Purpose

These conventions keep `Clean.Sdk` consistent and predictable across teams and features.

---

## Naming Conventions

### General

- Use `PascalCase` for classes, records, interfaces (with `I` prefix), methods, and public properties.
- Use `camelCase` for parameters and local variables.
- Use `_camelCase` for private fields.
- Use async suffix `Async` for asynchronous methods.

### Known Naming Inconsistencies

The following identifiers contain typos or inconsistencies and are scheduled for normalization in a future major version:

| Current | Should Be | Location |
|---------|-----------|----------|
| `AppExeption` | `AppException` | `Domain/Exceptions/AppExeption.cs` |
| `TServie` | `TService` | `Application/Handlers/*.cs` |
| `AplicacionHandlerAttribute` | `ApplicationHandlerAttribute` | `Application/Handlers/AplicacionHandlerAttribute.cs` |
| `GeyTypesByAttribute` | `GetTypesByAttribute` | `Domain/Helpers/AssemblyHelper.cs` |
| `SecctionName` | `SectionName` | `Infrastructure/Extensions/OptionsProvider.cs` |
| `dbConecction` | `dbConnection` | `Infrastructure/Extensions/EfCoreProvider.cs` |

> **Important:** Do not introduce new naming inconsistencies. Use the correct spelling in new code.

## Layer-Specific

### Domain

- Service interfaces: `I{Name}Service`
- Service implementations: `{Name}Service`
- Domain exceptions: `{Name}Exception`
- Validators: `{Name}Validator`

### Application

- Commands: `{Verb}{Entity}Command`
- Queries: `Get{Entity}Query`, `Get{Entity}ByIdQuery`
- Handlers: `{RequestName}Handler`
- DTOs: `{Entity}Dto`
- Profiles: `{Entity}Profile`

### Data

- Repository interfaces: `I{Entity}Repository`
- Repository implementations: `{Entity}Repository`
- Context classes: `{Name}DbContext`

---

## Marker Attributes

Use marker attributes for automatic registration:

| Attribute | Purpose | Scanned By |
|-----------|---------|------------|
| `[Service]` | Domain services | `AddDomainServices(assembly)` |
| `[Repository]` | Repositories | `AddRepositories(assembly)` |
| `[AplicacionHandler]` | Application handlers | `AddMediatR(assembly)` |
| `[MapperProfile]` | AutoMapper profiles | `AddAutoMapperProfiles(assembly)` |
| `[Option]` | Configuration option classes | `ConfigureAppSettingOptions<T>(...)` |

Every marked implementation must expose the expected interface contract.

> **Note:** The attribute name `AplicacionHandler` uses Spanish spelling. Normalization to `ApplicationHandler` is planned for a future major version.

---

## Folder Organization

Follow existing project boundaries:

- Domain concerns in `Clean.Sdk.Domain`
- Use-case orchestration in `Clean.Sdk.Application`
- Persistence in `Clean.Sdk.Data.EfCore`
- Composition and setup in `Clean.Sdk.Infrastructure`

Within each project, keep files grouped by feature or concern (services, handlers, validations, options, etc.).

---

## Class Layout (Recommended)

1. Constants
2. Static fields
3. Instance fields
4. Properties
5. Constructors
6. Public methods
7. Protected methods
8. Private methods

Keep constructor injection explicit and avoid hidden service resolution.

---

## Async and Cancellation

- Prefer `Task`/`Task<T>` for I/O-bound operations.
- Pass `CancellationToken` through service, handler, and repository layers.
- Avoid blocking calls (`.Result`, `.Wait()`) in asynchronous paths.

---

## Validation and Exceptions

- Validate null/empty values at boundaries.
- Use `ValidationSet` for aggregated validation failures.
- Throw domain-specific exceptions over generic `Exception`.
- Keep validation messages clear and actionable.

---

## Logging

- Use constructor-injected `ILogger<T>` through base classes and concrete implementations.
- Log decision points and failures, not trivial flow noise.
- Avoid logging sensitive data.

---

## Test Conventions

- Test class: `{ClassUnderTest}Tests`
- Test method: `Method_WhenCondition_ShouldResult`
- Arrange/Act/Assert structure is preferred.
- Keep unit tests deterministic and independent.

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
