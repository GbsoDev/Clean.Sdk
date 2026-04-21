# GEMINI.md - Clean.Sdk Context

## Project Overview
`Clean.Sdk` is a professional .NET 8 SDK designed for enterprise applications. It implements **Clean Architecture** and **CQRS** patterns to provide a reusable foundation for modern backend systems. It leverages EF Core for data abstraction and MediatR for request handling.

## Approach
- Think before acting. Read existing files before writing code.
- Be concise in output but thorough in reasoning.
- Prefer editing over rewriting whole files.
- Do not re-read files you have already read unless the file may have changed.
- Skip files over 100KB unless explicitly required.
- Suggest running /cost when a session is running long to monitor cache ratio.
- Recommend starting a new session when switching to an unrelated task.
- Test your code before declaring done.
- No sycophantic openers or closing fluff.
- Keep solutions simple and direct.
- User instructions always override this file.

### Key Technologies
- **Framework:** .NET 8.0 (C# 12)
- **Messaging/CQRS:** MediatR
- **Data Access:** Entity Framework Core (SQL Server, PostgreSQL, MySQL, InMemory)
- **Mapping:** AutoMapper
- **Validation:** FluentValidation & custom `ValidationSet`
- **Testing:** xUnit, Moq

---

## Architecture and Layering
The project follows a strict inward-dependency rule:
`Infrastructure -> [Application, Data.EfCore] -> Domain`

### Projects and Responsibilities
- **Clean.Sdk.Domain:** The core. Contains domain models (`IDomainModel`), service contracts, business logic, domain exceptions (`AppExeption`), and repository contracts (`IRepository`).
- **Clean.Sdk.Application:** Orchestration layer. Contains CQRS handlers (`SaveHandler`, `UpdateHandler`, etc.), DTOs, and AutoMapper profiles.
- **Clean.Sdk.Data.EfCore:** Persistence implementation. Contains the generic `EfRepository` and `EfDbContext`.
- **Clean.Sdk.Infrastructure:** Composition root. Contains DI extensions (`ServiceProvider`, `MediatRProvider`, etc.) and assembly scanning logic.

---

## Development Conventions

### Naming & Style
- **Classes/Methods:** `PascalCase`
- **Private Fields:** `_camelCase`
- **Async Methods:** Must have the `Async` suffix and accept a `CancellationToken`.
- **Interfaces:** Prefixed with `I`.

### Registration by Convention (Marker Attributes)
The SDK uses attributes to automatically register services in DI:
- `[Service]` -> Registered as Scoped by `AddDomainServices`.
- `[Repository]` -> Registered as Scoped by `AddRepositories`.
- `[AplicacionHandler]` -> Marker for MediatR handlers.
- `[MapperProfile]` -> Marker for AutoMapper profiles.
- `[Option]` -> For configuration classes.

### Important: Historical Naming Inconsistencies
The codebase contains several typos in public identifiers for historical reasons. **Do not fix these unless explicitly asked**, as they are breaking changes for the SDK consumers.
- `AppExeption` (instead of `AppException`)
- `TServie` (instead of `TService`) in generic handlers.
- `AplicacionHandlerAttribute` (instead of `ApplicationHandlerAttribute`).
- `GeyTypesByAttribute` (instead of `GetTypesByAttribute`).
- `SecctionName` and `dbConecction`.

---

## Building and Running

### Build Commands
```bash
# Standard build
dotnet build

# Formal build using scripts (respects dependency order)
./build.sh -c <Beta|Release>   # Linux/macOS
./build.ps1 -c <Beta|Release>  # Windows
```

### Testing
```bash
# Run all tests
dotnet test

# Run specific project
dotnet test Clean.Sdk.Domain.Tests
```

---

## Workflow Patterns

### Creating a New Service
1. Define model in `Domain.Model`.
2. Define interface in `Domain.Services` inheriting from `ISaveService`, `IUpdateService`, etc.
3. Implement in `Domain.Services` using `[Service]` attribute.
4. If it needs persistence, define `IRepository<T>` and implement in `Data.EfCore` with `[Repository]`.

### Creating a CQRS Handler
1. Create a Command/Query in `Application`.
2. Create a Handler inheriting from `SaveHandler`, `UpdateHandler`, or `QueryHandler`.
3. Decorate the handler with `[AplicacionHandler]`.

### Validation
- Use `ValidationSet` in the Domain layer to aggregate business rule failures.
- Use `FluentValidation` in the Application layer for request shape validation.
