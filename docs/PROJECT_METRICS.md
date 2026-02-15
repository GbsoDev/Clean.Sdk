# Project Metrics and Quality Indicators

## Metric Policy

This document uses **measured values** from the repository and test execution performed on the evidence date.

Evidence date: **2026-02-18**.

## Current Measured Repository Metrics

| Metric | Value |
|---|---:|
| Total projects | 8 |
| Production projects | 4 |
| Test projects | 4 |
| C# files (excluding `bin/obj`) | 115 |
| C# lines (estimated) | 4,207 |
| Docs files in `docs/` | 9 |
| Pipeline YAML files | 3 |
| Build configurations | Debug, Beta, Release |
| Target framework | .NET 8.0 |
| Test execution result | 112/112 passing |

## Layer Distribution

| Layer | Project | Purpose |
|---|---|---|
| Domain | `Clean.Sdk.Domain` | Business contracts, services, validation, exceptions |
| Application | `Clean.Sdk.Application` | CQRS handlers and use-case orchestration |
| Data | `Clean.Sdk.Data.EfCore` | EF Core repository implementation |
| Infrastructure | `Clean.Sdk.Infrastructure` | Dependency composition and framework integration |

## Dependency Ecosystem (Observed)

| Concern | Main Packages |
|---|---|
| CQRS | `MediatR` |
| Mapping | `AutoMapper`, `AutoMapper.Extensions.Microsoft.DependencyInjection` |
| Validation | `FluentValidation` |
| ORM | `Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.Relational` |
| DB providers | `SqlServer`, `MySql.EntityFrameworkCore`, `Npgsql`, `InMemory` |
| Testing | `xUnit`, `Moq`, `Moq.EntityFrameworkCore`, `coverlet.collector` |

## Delivery Indicators

- Non-Debug configurations generate NuGet packages automatically.
- Local package feed (`LocalFeed`) is created/registered when required.
- Packaging workflow is active in both `Beta` and `Release`.
- Build scripts enforce deterministic layer build order.

## KPI Baseline for Governance

### Engineering Quality

- Test pass rate
- Test flakiness rate
- Coverage by layer
- Static-analysis warnings trend

### Delivery

- Build success rate by branch
- Pull-request lead time
- Time to merge
- Post-release regression rate

### Maintainability

- Documentation freshness age
- Convention violations per pull request
- Architecture conformance issues over time

## Recommended Thresholds

- Test pass rate on main branch: `>= 98%`
- Critical pipeline build success: `>= 99%`
- Global coverage threshold: `>= 85%`
- Domain + Application coverage threshold: `>= 90%`
- Core docs freshness: `< 45 days`

## Data Integrity Rules

- Prefer CI artifacts as source of truth for quality metrics.
- Timestamp every metrics snapshot publication.
- Keep measurable data separate from qualitative assessment.
- Synchronize root `README.md`, `DOCUMENTATION_SUMMARY.md`, and this file on every metric refresh.
