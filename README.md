# Clean.Sdk

[![.NET Version](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-TBD-blue.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)](BUILD.md)
[![Version](https://img.shields.io/badge/version-1.0.12-blue.svg)](https://github.com/GbsoDev/Clean)
[![Tests](https://img.shields.io/badge/tests-112%2F112%20passing-brightgreen.svg)](#testing)

> Professional Clean Architecture SDK with CQRS for enterprise .NET 8 applications.

## Overview

`Clean.Sdk` is an EF Core data abstraction toolkit that applies Clean Architecture and CQRS patterns to provide a reusable, maintainable foundation for modern backend systems.

## Key Features

- ✅ **Clean Architecture** — clear separation of responsibilities by layer
- ✅ **CQRS Pattern** — command/query segregation with MediatR
- ✅ **Repository Pattern** — generic persistence abstraction
- ✅ **Multi-Database Support** — SQL Server, MySQL, PostgreSQL, InMemory
- ✅ **Auto-Registration** — attribute-based DI registration by convention
- ✅ **Testing-Oriented** — designed for high testability
- ✅ **Complete Documentation** — fully rewritten English documentation in `docs/`

## Project Status

```
┌─────────────────────────────────────────────────┐
│  Projects:                     8 projects       │
│  ├─ Production:                4 projects       │
│  └─ Testing:                   4 projects       │
├─────────────────────────────────────────────────┤
│  C# Files:                     115 files        │
│  Build Status:                 ✅ Successful    │
│  Tests (Evidence 2026-02-18):  112/112 (100%)  │
│  Target Framework:             .NET 8.0        │
│  Version:                      1.0.12 / beta   │
└─────────────────────────────────────────────────┘
```

## Solution Structure

```
Clean.Sdk/
├── Clean.Sdk.Domain                  # Domain layer
│   ├── Services/                     # Core service abstractions and implementations
│   ├── Validations/                  # Validation primitives and extensions
│   ├── Exceptions/                   # Domain exceptions
│   ├── Ports/                        # Contracts (e.g., IRepository)
│   └── Model/                        # Domain model contracts
│
├── Clean.Sdk.Application             # Application layer
│   ├── Handlers/                     # CQRS handlers (commands/queries)
│   ├── Mapper/                       # AutoMapper marker/profile support
│   └── Validations/                  # Application-level validation constants
│
├── Clean.Sdk.Data.EfCore             # Data layer
│   ├── EfRepository.cs               # Generic EF Core repository
│   ├── EfDbContext.cs                # Base DbContext abstraction implementation
│   └── Entities/                     # Data entity contracts/helpers
│
├── Clean.Sdk.Infrastructure          # Infrastructure/composition layer
│   ├── Extensions/                   # DI and framework providers
│   ├── Utilities/                    # JSON converters and utility components
│   └── LazyServiceProvider.cs        # Lazy service factory
│
├── *.Tests/                          # Test projects by layer
└── docs/                             # English documentation set
```

## Quick Start

### Install and Build

```bash
# Clone repository
git clone https://github.com/GbsoDev/Clean.git
cd Clean.Sdk

# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run tests
dotnet test
```

### Basic Usage

```csharp
// Program.cs - SDK configuration
var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetExecutingAssembly();

builder.Services
    .AddDomainServices(assembly)
    .AddRepositories(assembly)
    .AddMediatR(assembly)                   // registers [AplicacionHandler] handlers
    .AddAutoMapperProfiles(assembly);       // registers [MapperProfile] profiles

builder.Services
    .AddEfCoreContext<IMyDbContext, MyDbContext>(dbConnection);

builder.Services
    .ConfigureAppSettingOptions<AppSettings>(ref builder.Configuration, out var appSettings)
    .AddWebApiAutenticacionToken(appSettings)
    .AddWebApiCorsPolicies(appSettings)
    .AddLazySupport();

var app = builder.Build();
app.Run();
```

## Documentation

The official documentation is in `docs/`:

- [docs/README.md](docs/README.md) — Documentation entry point
- [docs/DOCUMENTATION_STANDARD.md](docs/DOCUMENTATION_STANDARD.md) — Standard baseline and compliance mapping
- [docs/INDEX.md](docs/INDEX.md) — Navigation map and reading paths
- [docs/PROJECT_ANALYSIS.md](docs/PROJECT_ANALYSIS.md) — Full project assessment
- [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) — Layered architecture and flow
- [docs/DEVELOPMENT_GUIDE.md](docs/DEVELOPMENT_GUIDE.md) — Practical implementation guide
- [docs/CONVENTIONS.md](docs/CONVENTIONS.md) — Coding and design conventions
- [docs/QUICK_REFERENCE.md](docs/QUICK_REFERENCE.md) — Daily-use cheat sheet
- [docs/PROJECT_METRICS.md](docs/PROJECT_METRICS.md) — Measured metrics and KPI recommendations
- [DOCUMENTATION_SUMMARY.md](DOCUMENTATION_SUMMARY.md) — Root documentation summary

## Testing

### Run Tests

```bash
# All tests
dotnet test

# With coverage
dotnet test /p:CollectCoverage=true

# Specific project
dotnet test Clean.Sdk.Domain.Tests
```

### Current Reported Test State

- Total tests: `112`
- Passing: `112` (`100%`)
- Failing: `0`
- Notes: values correspond to the latest local evidence run on `2026-02-18`

## Technology Stack

### Core
- `.NET 8.0`
- `C# 12`

### Application & Domain
- `MediatR` `12.1.1`
- `AutoMapper` `12.0.1`
- `FluentValidation` `11.9.0`
- `Microsoft.Extensions.Logging` `8.0.0`

### Data & Infrastructure
- `Entity Framework Core` `8.0.4`
- `Microsoft.EntityFrameworkCore.SqlServer` `8.0.4`
- `MySql.EntityFrameworkCore` `8.0.0`
- `Npgsql.EntityFrameworkCore.PostgreSQL` `8.0.4`
- `Microsoft.EntityFrameworkCore.InMemory` `8.0.4`
- `Microsoft.AspNetCore.Authentication.JwtBearer` `8.0.0`

### Testing
- `xUnit`
- `Moq`
- `Moq.EntityFrameworkCore`
- `coverlet.collector`

## Build Configurations

```
Debug:    Local development using project references
Beta:     1.0.13-beta package flow to LocalFeed
Release:  1.0.12 package flow to LocalFeed
```

Build scripts are documented in [BUILD.md](BUILD.md).

## Contributing

Contributions are welcome:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/NewFeature`)
3. Commit your changes (`git commit -m "Add NewFeature"`)
4. Push the branch (`git push origin feature/NewFeature`)
5. Open a Pull Request

## License

TBD.

## Author

**Gerson Sánchez**
- Company: GBSO Dev
- GitHub: [@GbsoDev](https://github.com/GbsoDev)
- Repository: https://github.com/GbsoDev/Clean

---

Version: `1.0.12` (Release) / `1.0.13-beta` (Beta)  
Framework: `.NET 8.0`
