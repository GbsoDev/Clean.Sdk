# Clean.Sdk Documentation Index

This index is the canonical navigation map for repository documentation.

## Documentation Baseline

- [DOCUMENTATION_STANDARD.md](DOCUMENTATION_STANDARD.md) — Standards, compliance mapping, and governance.

## Main Technical Documents

1. [PROJECT_ANALYSIS.md](PROJECT_ANALYSIS.md)  
   Full project assessment: status, quality, risks, debt, and roadmap.

2. [ARCHITECTURE.md](ARCHITECTURE.md)  
   Architecture description aligned with ISO/IEC/IEEE 42010 (stakeholders, concerns, viewpoints, rationale).

3. [DEVELOPMENT_GUIDE.md](DEVELOPMENT_GUIDE.md)  
   Contributor implementation workflows and extension patterns.

4. [CONVENTIONS.md](CONVENTIONS.md)  
   Naming, layering, coding, validation, and testing conventions.

5. [QUICK_REFERENCE.md](QUICK_REFERENCE.md)  
   Operational commands and reusable implementation snippets.

6. [PROJECT_METRICS.md](PROJECT_METRICS.md)  
   Measured repository metrics, quality indicators, and KPI targets.

## Audience Reading Paths

### New Contributors
1. `PROJECT_ANALYSIS.md`
2. `ARCHITECTURE.md`
3. `QUICK_REFERENCE.md`
4. `DEVELOPMENT_GUIDE.md`
5. `CONVENTIONS.md`

### Tech Leads / Architects
1. `PROJECT_ANALYSIS.md`
2. `ARCHITECTURE.md`
3. `PROJECT_METRICS.md`
4. `DOCUMENTATION_STANDARD.md`

### Maintainers / CI Owners
1. `PROJECT_METRICS.md`
2. `DEVELOPMENT_GUIDE.md`
3. `CONVENTIONS.md`
4. `BUILD.md` (repository root)

## Current Snapshot (Evidence Date: 2026-04-07)

- Runtime: `.NET 8.0`
- Projects: `8` (`4` production + `4` test)
- C# files (excluding `bin/obj`): `161`
- C# lines (estimated): `5,277`
- Test status: `112/112` passing
- Build profiles: `Debug`, `Beta`, `Release`

## Code Entry Points

- Domain layer: `Clean.Sdk.Domain/`
- Application layer: `Clean.Sdk.Application/`
- Data layer: `Clean.Sdk.Data.EfCore/`
- Infrastructure layer: `Clean.Sdk.Infrastructure/`
- Tests: `*.Tests/`
