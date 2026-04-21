# Clean.Sdk Roadmap

This document outlines the planned improvements and architectural debt resolution for `Clean.Sdk`.

---

## Priority Improvement Backlog

### Horizon 1: Immediate (1-2 sprints)

- **Normalize Typo-Prone Identifiers:** (Breaking change, requires major version bump)
  - `AppExeption` → `AppException` (`Domain/Exceptions/AppExeption.cs`)
  - `TServie` → `TService` (`Application/Handlers/SaveHandler.cs`, `UpdateHandler.cs`, `CommandDeleteByIdHandler.cs`)
  - `AplicacionHandlerAttribute` → `ApplicationHandlerAttribute` (`Application/Handlers/AplicacionHandlerAttribute.cs`)
  - `GeyTypesByAttribute` → `GetTypesByAttribute` (`Domain/Helpers/AssemblyHelper.cs`)
  - `SecctionName` → `SectionName` (`Infrastructure/Extensions/OptionsProvider.cs`)
  - `dbConecction` → `dbConnection` (`Infrastructure/Extensions/EfCoreProvider.cs`)
- **Resolve `CrudService` Status:** Complete the implementation or remove it; document the composed service-base pattern (`SaveService`/`UpdateService`/`DeleteService`) as the canonical approach.
- **CI Quality Gates:** Add CI policy section for minimum quality gates (test pass, analyzers, formatting).
- **Architecture Decision Records (ADR):** Add explicit ADRs for key design choices and critical trade-offs.

### Horizon 2: Near-Term (quarter)

- **Integration Testing:** Increase integration-oriented tests for EF Core repository behavior (currently mock-based) to reduce mock-only confidence risk.
- **Metadata Centralization:** Centralize package/build metadata shared across production projects where feasible.
- **Operational Guidance:** Publish guidance for diagnostics, failure triage, and observability (logs/events/diagnostics).
- **Logger Improvement:** Address `ILoggerService` category limitation (currently using a shared category via port implementation).

### Horizon 3: Mid-Term & Long-Term

- **Architecture Conformance:** Add automation in CI to validate dependencies and naming conventions.
- **KPI Dashboarding:** Introduce trend-based dashboarding for engineering KPIs.
