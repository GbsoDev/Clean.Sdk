# Clean.Sdk Architecture Description

## Architecture Description Scope

This document describes the software architecture of `Clean.Sdk` using a structure aligned with **ISO/IEC/IEEE 42010**.

Scope includes:

- Layer model and dependency flow
- Responsibilities by project
- Runtime interaction patterns (CQRS lifecycle)
- Cross-cutting mechanisms (validation, DI registration, packaging)
- Architectural constraints, risks, and decisions

Evidence date: **2026-04-07**.

## System Context

`Clean.Sdk` is a reusable .NET SDK intended to standardize backend architecture concerns for enterprise services by combining:

- Clean Architecture layering
- CQRS request handling with MediatR
- Repository abstraction over EF Core
- Convention-based DI registration

## Stakeholders and Concerns

### Stakeholders

- Application developers integrating the SDK
- Maintainers of shared abstractions and build pipelines
- Solution architects defining layering and dependency constraints
- QA/CI owners responsible for reliability and quality gates

### Main Concerns

- Enforcing clear dependency direction
- Preserving business-rule isolation from persistence/framework details
- Maintaining extensibility without uncontrolled complexity
- Keeping build/release reproducible across configurations
- Keeping documentation and metrics synchronized with repository reality

## Viewpoints and Views

## 1) Layer Viewpoint (Static Structure)

### Dependency Direction

```
Infrastructure (composition root, providers, registration)
                ↓
Application (CQRS handlers, orchestration, mapping)
                ↓
Domain (contracts, services, validations, exceptions)
                ↑
Data.EfCore (repository implementation and persistence concerns)
```

Current project dependency behavior in solution and project files:

- `Clean.Sdk.Application` depends on `Clean.Sdk.Domain`
- `Clean.Sdk.Data.EfCore` depends on `Clean.Sdk.Domain`
- `Clean.Sdk.Infrastructure` depends on `Clean.Sdk.Application`, `Clean.Sdk.Data.EfCore`, and `Clean.Sdk.Domain`

## 2) Responsibility Viewpoint (By Project)

### `Clean.Sdk.Domain`

- Domain contracts and model abstractions
- Service base classes and service contracts
- Validation primitives (`ValidationSet`, argument validation helpers)
- Exception hierarchy and options contracts
- Repository contracts (`IRepository<TModel>`)
- Logging abstraction (`ILoggerService`)

#### Service Base Class Hierarchy

```
Service
  └─ ActionService<TModel, TRepo>        ← exposes protected Repository
       ├─ SaveService<TModel, TRepo>      ← ISaveService<TModel>  (SaveAsync)
       ├─ UpdateService<TModel, TRepo>    ← IUpdateService<TModel> (UpdateAsync)
       └─ DeleteService<TModel, TRepo>    ← IDeleteService<TModel> (DeleteByIdAsync, DeleteAsync)

CrudService<TModel, TRepo>              [Obsolete — do not use]
```

`IDeleteService<TModel>` exposes two methods:
- `DeleteByIdAsync(object id, CancellationToken)` — looks up entity by PK before deleting
- `DeleteAsync(TModel entity, CancellationToken)` — deletes a pre-loaded entity instance

### `Clean.Sdk.Application`

- Command/query abstractions and handlers
- CQRS orchestration with MediatR
- Mapping boundary (AutoMapper)
- Handler-level validation orchestration

**Known Limitations:**
- Generic type parameter `TServie` contains typo (should be `TService`)

### `Clean.Sdk.Data.EfCore`

- EF Core repository implementation
- DbContext abstraction and persistence execution
- Entity state and persistence timestamp behavior

**Known Limitations:**
- `UpdateAsync` does not call `SaveChangesAsync` implicitly (caller must save explicitly)
- `DeleteByIdAsync` performs entity lookup before deletion (may be inefficient for bulk operations)

### `Clean.Sdk.Infrastructure`

- Assembly scanning and dependency registration
- Provider extensions for services, repositories, options, handlers, mappers
- Web/serialization/security integration helpers
- Lazy service provider facilities

**Known Issues:**
- Method name `GeyTypesByAttribute` contains typo (should be `GetTypesByAttribute`)
- Parameter name `dbConecction` contains typo (should be `dbConnection`)
- Parameter name `SecctionName` contains typo (should be `SectionName`)

Key extension methods by provider class:

| Provider | Method |
|---|---|
| `ServiceProvider` | `AddDomainServices(assembly)` |
| `DataRepositoryProvider` | `AddRepositories(assembly)` |
| `MediatRProvider` | `AddMediatR(assembly)` |
| `AutoMapperProvider` | `AddAutoMapperProfiles(assembly)` |
| `EfCoreProvider` | `AddEfCoreContext<TContext, TImpl>(dbConnection)`, `MigrateDataBase<TDbContext>(dbConnection)` |
| `OptionsProvider` | `ConfigureAppSettingOptions<T>(ref configuration, out T appSettings)` |
| `WebApiProvider` | `AddWebApiAutenticacionToken(appSettings)`, `AddWebApiCorsPolicies(appSettings)` |
| `LazyProvider` | `AddLazySupport()` |

## 3) Behavioral Viewpoint (Typical Write Flow)

1. API/controller sends command via MediatR.
2. Application handler executes validation rules (FluentValidation with RuleSets).
3. Request maps to domain model (AutoMapper).
4. Domain service executes business logic (ValidationSet for invariants).
5. Repository abstraction persists through EF Core implementation.
6. Response maps to DTO and returns to caller.

> **Note:** Handlers now utilize a decoupled `ILoggerService` port via `Lazy<T>` injection, improving architectural purity and testability.

## 4) Cross-Cutting Viewpoint

### Validation Model

- Application layer validates input shape and command/query constraints.
- Domain layer validates business invariants and aggregate rules.
- Data layer enforces persistence-level constraints.
- `ValidationSet` aggregates errors for deterministic exception handling.

### Registration Model

Convention-driven and attribute-guided registration supports automatic discovery for:

- `[Service]` → scanned by `AddDomainServices(assembly)`
- `[Repository]` → scanned by `AddRepositories(assembly)`
- `[AplicacionHandler]` → marker only; MediatR handler discovery is done by `AddMediatR(assembly)`
- `[MapperProfile]` → scanned by `AddAutoMapperProfiles(assembly)`
- `[Option]` → scanned by `ConfigureAppSettingOptions<T>(...)` via `OptionsProvider`

### Build and Packaging Model

For non-`Debug` configurations, production projects:

- Generate NuGet packages
- Ensure and register `LocalFeed`
- Push built package to local source

Scripted build order:

1. `Clean.Sdk.Domain`
2. `Clean.Sdk.Application`
3. `Clean.Sdk.Data.EfCore`
4. `Clean.Sdk.Infrastructure`

## Architectural Constraints

- Business rules remain in `Domain`.
- Handlers orchestrate use cases and avoid persistence logic.
- Repositories avoid business policy implementation.
- Dependency flow must preserve inward architecture direction.
- New modules must adopt existing registration and naming conventions.

## Architectural Decisions and Rationale

1. **Layered architecture with contracts-first domain**  
    Rationale: improve maintainability, testability, and long-term separation of concerns.

2. **CQRS handler base classes**  
    Rationale: standardize request orchestration and reduce repetitive implementation code.

3. **Attribute-based registration**  
    Rationale: simplify wiring and reduce manual DI boilerplate.

4. **EF Core repository implementation behind contracts**  
    Rationale: keep persistence implementation replaceable from domain/application perspective.

## Trade-Offs and Risks

- Generic base classes improve reuse but can increase abstraction depth for new contributors.
- Attribute scanning accelerates setup but can make registration failures less visible.
- Local-feed packaging in build targets simplifies delivery but requires strict environment consistency.
- Historical naming inconsistencies in some identifiers increase cognitive load.

## Architecture Evolution and Roadmap

For the detailed list of planned improvements, architectural debt resolution, and the project roadmap, please refer to:

- [ROADMAP.md](ROADMAP.md)

## Conformance Statement

This document follows ISO/IEC/IEEE 42010 principles by explicitly describing:

- stakeholders and concerns,
- architecture viewpoints and corresponding views,
- decisions and rationale,
- constraints, trade-offs, and evolution risks.
