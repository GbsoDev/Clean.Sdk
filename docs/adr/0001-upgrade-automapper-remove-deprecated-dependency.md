# ADR 0001: Upgrade AutoMapper to 16.1.1 and Remove Deprecated Extension Package

## Status

Accepted

## Accepted Date

2026-05-12

## Date

2026-05-12

## Context

- Clean.Sdk.Infrastructure 1.0.13 depends on `AutoMapper.Extensions.Microsoft.DependencyInjection` 12.0.1, which is deprecated since AutoMapper 13.0.0 (functionality merged into the main `AutoMapper` package).
- Clean.Sdk.Application 1.0.13 and Clean.Sdk.Data.EfCore 1.0.13 depend on `AutoMapper` 12.0.1.
- AutoMapper 12.0.1 has CVE-2026-32933 (DoS via uncontrolled recursion, CVSS 7.5 High). All versions < 15.1.1 are affected.
- The deprecated extension package forces consuming projects (e.g., finance-dotnet-webapi) to get NU1608 warnings when they upgrade AutoMapper independently.
- Clean.Sdk also pins older `Microsoft.Extensions.*` packages at 8.0.0/8.0.4, which causes NU1605 downgrade errors when consuming projects upgrade to 10.0.x.

## Decision

Apply these changes to Clean.Sdk:

1. **Clean.Sdk.Application.csproj**: Upgrade `AutoMapper` from 12.0.1 → 16.1.1
2. **Clean.Sdk.Data.EfCore.csproj**: Upgrade `AutoMapper` from 12.0.1 → 16.1.1
3. **Clean.Sdk.Infrastructure.csproj**: 
   - Remove `AutoMapper.Extensions.Microsoft.DependencyInjection` 12.0.1 (deprecated)
   - Add `AutoMapper` 16.1.1 (replaces the removed extension)
   - Bump `Microsoft.Extensions.*` to 10.0.0 to avoid NU1605 in consumers
4. Rebuild all Clean.Sdk projects and republish to local NuGet feed
5. Remove any NU1608 suppressions from consuming projects

## Implementation Notes

During implementation, three additional changes were required beyond the original decision:

1. **Clean.Sdk.Application.csproj**: `Microsoft.Extensions.DependencyInjection.Abstractions` had to be bumped from 8.0.0 to 10.0.0 because AutoMapper 16.1.1 transitively requires `>= 10.0.0` via `Microsoft.Extensions.Logging.Abstractions`. Without this bump, NU1605 downgrade errors occur.

2. **Clean.Sdk.Application.Tests.csproj**: Same `Microsoft.Extensions.DependencyInjection.Abstractions` bump for consistency, avoiding transitive NU1605 in test projects.

3. **AutoMapperProvider.cs**: The `AddAutoMapper(Type[])` overload was removed in AutoMapper 16.1.1. The code was refactored to use `AddAutoMapper(Action<IMapperConfigurationExpression>)` with `cfg.AddProfile(profileType)` for each profile type discovered via `MapperProfileAttribute` scanning. The public API signature `AddAutoMapperProfiles(IServiceCollection, Assembly)` is preserved.

## Alternatives Considered

- **Keep current versions and suppress warnings in consumers**: Propagates the CVE and deprecation downstream.
- **Upgrade only AutoMapper without touching Microsoft.Extensions**: May work but consumers would still face version mismatch warnings.

## Consequences

### Positive
- Eliminates CVE-2026-32933 at the SDK level.
- Removes deprecated package, reducing dependency surface.
- Cleans up downstream NU1608 and NU1605 warnings in consuming projects.

### Negative
- Requires republishing all Clean.Sdk packages to local feed.
- Bumping version number is necessary to propagate the changes.

### Neutral
- `AddAutoMapper(Type[])` overload was removed in 16.1.1; code was refactored to use `AddAutoMapper(Action<IMapperConfigurationExpression>)` with `cfg.AddProfile()`. Public API `AddAutoMapperProfiles(IServiceCollection, Assembly)` is preserved.
- Microsoft.Extensions.* 10.0.x is within the `>= 8.0.0` constraints already specified by Clean.Sdk.
