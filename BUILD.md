# Build Scripts Documentation

## Overview

This solution includes automated build scripts that handle the compilation of all projects in the correct dependency order. These scripts are designed for local development and Azure DevOps pipelines.

## Project Build Order

The scripts compile projects in the following order to respect dependencies:

1. **Clean.Sdk.Domain** (base layer, no dependencies)
2. **Clean.Sdk.Application** (depends on Domain)
3. **Clean.Sdk.Data.EfCore** (depends on Domain)
4. **Clean.Sdk.Infrastructure** (depends on Domain, Application, and Data.EfCore)

## Usage

### Windows / PowerShell

```powershell
./build.ps1 --config <Beta|Release>
```

Or using the short form:

```powershell
./build.ps1 -c <Beta|Release>
```

**Examples:**

```powershell
./build.ps1 --config Beta
./build.ps1 -c Release
```

### Linux / macOS / Bash

```bash
./build.sh --config <Beta|Release>
```

Or using the short form:

```bash
./build.sh -c <Beta|Release>
```

**Examples:**

```bash
./build.sh --config Beta
./build.sh -c Release
```

**Note:** On Linux/macOS, ensure the script has execution permissions:

```bash
chmod +x build.sh
```

## Parameters

| Parameter | Short | Required | Values | Description |
|-----------|-------|----------|--------|-------------|
| --config | -c | Yes | Beta, Release | Build configuration to use |

## Behavior

- Each project is built using `dotnet build` with the specified configuration
- Console output is displayed in real-time
- If any project fails to build, the script stops immediately and exits with a non-zero code
- Projects configured to generate NuGet packages will automatically pack and publish to the local feed after building (configured in .csproj files)

## Error Handling

The scripts implement strict error handling:

- Missing project files trigger immediate failure
- Build failures stop the entire process
- Exit codes are propagated for CI/CD integration

## Azure DevOps Integration

These scripts are compatible with Azure DevOps pipelines:

**Windows Agent:**

```yaml
- task: PowerShell@2
  inputs:
    filePath: 'build.ps1'
    arguments: '-config Release'
```

**Linux Agent:**

```yaml
- script: |
    chmod +x build.sh
    ./build.sh --config Release
  displayName: 'Build all projects'
```

## Notes

- Scripts use UTF-8 encoding without BOM for cross-platform compatibility
- No external dependencies required beyond .NET SDK
- The scripts do NOT execute `dotnet pack` or `dotnet nuget push` directly - these are handled by MSBuild targets in the .csproj files when building in Beta or Release configurations
