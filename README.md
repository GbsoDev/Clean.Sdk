# Clean.Sdk

[![.NET Version](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)](docs/CONTRIBUTING.md#build-scripts)
[![Tests](https://img.shields.io/badge/tests-112%2F112%20passing-brightgreen.svg)](docs/CONTRIBUTING.md#testing-guidance)

Professional Clean Architecture SDK with CQRS for enterprise .NET 8 applications.

## Overview

`Clean.Sdk` is a powerful toolkit designed to standardize backend development by enforcing Clean Architecture, CQRS (via MediatR), and the Repository pattern. It simplifies the creation of maintainable, scalable, and highly testable .NET 8 systems.

## Key Features

- ✅ **Clean Architecture** — Strict layer isolation and inward dependency flow.
- ✅ **CQRS Pattern** — Standardized request handling with MediatR.
- ✅ **Repository Pattern** — Generic persistence abstraction over EF Core.
- ✅ **Multi-Database Support** — SQL Server, MySQL, PostgreSQL, and InMemory.
- ✅ **Convention-Based Registration** — Automatic DI wiring via marker attributes.
- ✅ **Comprehensive Documentation** — ISO/IEC/IEEE aligned architecture and guides.

## Solution Structure

- **[Clean.Sdk.Domain](Clean.Sdk.Domain/)**: Business models, service contracts, validations, and ports.
- **[Clean.Sdk.Application](Clean.Sdk.Application/)**: CQRS commands/queries, handlers, and mapping profiles.
- **[Clean.Sdk.Data.EfCore](Clean.Sdk.Data.EfCore/)**: Generic EF Core repository and DbContext implementation.
- **[Clean.Sdk.Infrastructure](Clean.Sdk.Infrastructure/)**: DI composition root, providers, and framework extensions.
- **[docs/](docs/)**: Detailed architectural, contributing, and reference documentation.

## Documentation

Detailed documentation is available in the `docs/` directory:

- [Architecture Description](docs/ARCHITECTURE.md) — Design principles and layer views.
- [Contributing & Conventions](docs/CONTRIBUTING.md) — Development workflow and style guides.
- [Quick Reference](docs/QUICK_REFERENCE.md) — Commands and implementation snippets.
- [Roadmap](docs/ROADMAP.md) — Planned features and technical debt resolution.

## Quick Start

### Build & Test

```bash
# Clone and build
git clone https://github.com/GbsoDev/Clean.git
cd Clean.Sdk
dotnet restore
dotnet build

# Run all tests
dotnet test
```

### Clean Build

To perform a clean build (recommended when switching configurations or resolving package conflicts):

#### Linux/macOS

```bash
# Clean local NuGet packages (optional)
rm -rf ~/.nuget/local-packages/*

# Deep clean bin/obj folders
find . -type d \( -name bin -o -name obj \) -exec rm -rf {} +

# Standard dotnet clean
dotnet clean ./Clean.Sdk.sln

# Build using the provided script
chmod +x build.sh
./build.sh --config Release
```

#### Windows (PowerShell)

```powershell
# Clean local NuGet packages (optional)
Remove-Item -Path "$HOME\.nuget\local-packages\*" -Force
dotnet nuget remove source LocalFeed

# Deep clean bin/obj folders
Remove-Item -Path .\*\bin -Recurse -Force
Remove-Item -Path .\*\obj -Recurse -Force

# Standard dotnet clean
dotnet clean .\Clean.Sdk.sln

# Build using the provided script
.\build.ps1 -c Release
```

### Formal Build Scripts

The SDK includes scripts to build projects in the correct dependency order:

- **Linux/macOS:** `./build.sh --config <Beta|Release>`
- **Windows:** `./build.ps1 --config <Beta|Release>`

## Registration Example

```csharp
var assembly = Assembly.GetExecutingAssembly();

// Standardize layers in DI
builder.Services
    .AddDomainServices(assembly)
    .AddRepositories(assembly)
    .AddMediatR(assembly)
    .AddAutoMapperProfiles(assembly)
    .AddLazySupport();

// Configure EF Core and Options
builder.Services.AddEfCoreContext<IMyDbContext, MyDbContext>(dbConnection);
builder.Services.ConfigureAppSettingOptions<AppSettings>(ref configuration, out var appSettings);
```

## Development Notes

### NuGet Feed Conflict
In the development environment, NuGet may prioritize the global package cache (`~/.nuget/packages`) over the `LocalFeed` if they contain the same version number. This can cause synchronization issues where changes are not reflected in consuming projects.

**To resolve this conflict:**
- **Increment the project version:** Update the `<Version>` tag in the `.csproj` file to force NuGet to treat it as a new package.
- **Clear the NuGet global cache:** Run `dotnet nuget locals all --clear`.
- **Manual deletion:** Delete the specific package folder from `%USERPROFILE%\.nuget\packages\`.

## Contributing

Please read [docs/CONTRIBUTING.md](docs/CONTRIBUTING.md) for details on our development workflow and coding standards.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**Version:** 1.0.12 (Release) / 1.0.13-beta (Beta)  
**Evidence Date:** 2026-04-07  
**Files:** 116 C# files | 112/112 tests passing
