# CI/CD Pipelines - Clean.Sdk

## 🌳 Branching Strategy: Simplified GitFlow

### Branches and Configurations

| Branch | Purpose | Build Config | NuGet Suffix | CI Trigger | CD Trigger | Stability |
|------|-----------|--------------|--------------|------------|------------|-------------|
| **main** | Production | `Release` | *(none)* | ✅ | ✅ | 🟢 Stable |
| **staging** | Pre-production/Testing | `Beta` | `-beta` | ✅ | ✅ | 🟡 Testing |
| **develop** | Active development | `Debug` | `-dev` | ✅ | ❌ | 🔴 Unstable |

---

## 📋 CI/CD Rules

### **CI Pipeline (Continuous Integration)**
**File:** `Clean.Sdk-CI.yml`

#### Automatic Triggers:
- ✅ Push to `main`, `staging`, `develop`
- ✅ Pull Requests to `main`, `staging`, `develop`
- ✅ Changes under paths: `Clean.Sdk.*/*`

#### Process:
1. **Build** - Runs `build.sh` to compile all projects in the correct order
   - Domain → Application + Data.EfCore → Infrastructure
   - The script handles dependencies and ordering automatically
2. **Test** - Runs all solution tests in one pass
   - Uses `dotnet test` on `Clean.Skd.sln`
   - Includes `--no-build --no-restore` for efficiency

#### Build Configuration by Branch:
```yaml
main     → Release (production-optimized)
staging  → Beta (testing-optimized)
develop  → Debug (with debug symbols)
```

#### Advantages:
- ✅ Faster and more efficient compilation
- ✅ Dependency order guaranteed by `build.ps1`/`build.sh`
- ✅ Tests executed across the full solution
- ✅ No duplicated restore/clean operations

---

### **CD Pipeline (Continuous Deployment)**
**File:** `Clean.Sdk-CD.yml`

#### Automatic Triggers:
- ✅ Runs when CI completes successfully on `main` or `staging`
- ❌ Does NOT run automatically for `develop`

#### Process:
1. **Build** all projects in dependency order
2. **Pack** using versions defined in each `.csproj`
3. **Publish** to Azure Artifacts feed: `Gbso.Clean.Sdk/NuGets`

#### Versioning:
```
Versions are defined in each .csproj file using:
<Version Condition="'$(Configuration)' == 'Beta'">1.0.13-beta</Version>
<Version Condition="'$(Configuration)' != 'Beta'">1.0.12</Version>

Examples:
  main (Release)    → 1.0.12       (production)
  staging (Beta)    → 1.0.13-beta  (testing)
```

**Important:** Versions must be updated manually in each project's `.csproj` file.

---

## 🚀 Recommended Workflow

### 1. Daily Development (`develop`)
```bash
git checkout develop
git pull
# make changes
git add .
git commit -m "feat: new feature"
git push origin develop
```
- ✅ CI runs build and tests automatically
- ❌ Does not generate NuGet packages

### 2. Testing/QA (`staging`)
```bash
# First update Beta versions in .csproj files (ex: 1.1.0-beta)
git add .
git commit -m "chore: bump version to x.x.x-beta"
git push origin develop

git checkout staging
git merge develop
git push origin staging
```
- ✅ CI runs build and tests
- ✅ CD generates NuGet packages with `-beta` suffix
- 📦 Published to Azure Artifacts

### 3. Production (`main`)
```bash
# After successful testing, update Release versions (without -beta)
git checkout staging
# Edit .csproj files to update Release version
git add .
git commit -m "chore: bump version to x.x.x"
git push origin staging

git checkout main
git merge staging
git push origin main
```
- ✅ CI runs build and tests
- ✅ CD generates NuGet packages **without suffix**
- 📦 Published to Azure Artifacts (ready for NuGet.org)

---

## 📦 Projects and Dependencies

```
Clean.Sdk.Domain (base)
   ↓
├─→ Clean.Sdk.Data.EfCore
├─→ Clean.Sdk.Application
   ↓
└─→ Clean.Sdk.Infrastructure
```

**CD Build Order:**
1. Domain (first, no dependencies)
2. EfCore + Application (in parallel, both depend on Domain)
3. Infrastructure (last, depends on Application)

---

## 🔧 Technical Configuration

### Execution Pool:
- **self-hosted** agents

### Publish Target:
- **Azure Artifacts:** `Gbso.Clean.Sdk/NuGets`
- **NuGet.org:** (configured, not automatic)

### Templates:
- `Clean.Sdk-CD-Template.yml` - Reusable template for CD jobs (build & pack)

---

## ⚠️ Important

1. **Do NOT push directly to `main`** - always promote through `staging` first
2. **Versions must be updated manually** in each `.csproj` before release
3. **Use SemVer versioning**: `Major.Minor.Patch` or `Major.Minor.Patch-suffix`
4. **Increment version based on change type:**
   - **Patch** (x.x.N): bug fixes, minor changes
   - **Minor** (x.N.0): backward-compatible new features
   - **Major** (N.0.0): breaking changes
5. **Keep version consistency** across all projects

---

## 🧩 How to Update Versions

### Before a release, update versions in each `.csproj`:

```xml
<!-- Clean.Sdk.Domain/Clean.Sdk.Domain.csproj -->
<PropertyGroup>
  <Version Condition="'$(Configuration)' == 'Beta'">1.1.0-beta</Version>
  <Version Condition="'$(Configuration)' != 'Beta'">1.0.15</Version>
</PropertyGroup>
```

### Recommended Steps:

1. **On `develop` branch**, update the Beta version in all 4 projects:
   ```bash
   # Edit versions in these .csproj files
   Clean.Sdk.Domain/Clean.Sdk.Domain.csproj
   Clean.Sdk.Data.EfCore/Clean.Sdk.Data.EfCore.csproj
   Clean.Sdk.Application/Clean.Sdk.Application.csproj
   Clean.Sdk.Infrastructure/Clean.Sdk.Infrastructure.csproj
   ```

2. **Commit version changes:**
   ```bash
   git add .
   git commit -m "chore: bump version to 1.1.0-beta"
   git push origin develop
   ```

3. **Merge to `staging` for testing:**
   ```bash
   git checkout staging
   git merge develop
   git push origin staging
   # CD publishes packages with x.x.x-beta version
   ```

4. **After successful testing, update Release version and merge to `main`:**
   ```bash
   # Update Release version in .csproj files (without -beta suffix)
   git commit -m "chore: bump version to 1.1.0"
   git push origin staging

   git checkout main
   git merge staging
   git push origin main
   # CD publishes packages with x.x.x version (production)
   ```

### Helper Script to Update Versions (PowerShell):

```powershell
# update-versions.ps1
param(
    [string]$BetaVersion = "1.1.0-beta",
    [string]$ReleaseVersion = "1.1.0"
)

$projects = @(
    "Clean.Sdk.Domain",
    "Clean.Sdk.Data.EfCore",
    "Clean.Sdk.Application",
    "Clean.Sdk.Infrastructure"
)

foreach ($project in $projects) {
    $csprojPath = "$project/$project.csproj"
    Write-Host "Updating $csprojPath..."

    $content = Get-Content $csprojPath -Raw
    $content = $content -replace '<Version Condition="''?\$\(Configuration\)''? == ''?Beta''?">[^<]+</Version>', "<Version Condition=`"'`$(Configuration)' == 'Beta'`">$BetaVersion</Version>"
    $content = $content -replace '<Version Condition="''?\$\(Configuration\)''? != ''?Beta''?">[^<]+</Version>', "<Version Condition=`"'`$(Configuration)' != 'Beta'`">$ReleaseVersion</Version>"

    Set-Content -Path $csprojPath -Value $content
}

Write-Host "✅ Versions updated successfully!"
```

**Usage:**
```powershell
.\update-versions.ps1 -BetaVersion "1.2.0-beta" -ReleaseVersion "1.2.0"
```

---

## 🐛 Debugging

### If CI fails:
- Verify all tests pass locally
- Verify there are no compilation errors
- Review pipeline logs in Azure DevOps

### If CD fails:
- Verify CI completed successfully
- Verify Azure Artifacts permissions
- Verify `.csproj` versions are valid (SemVer format)
- Verify there are no conflicts with already-published versions in the feed

### To run local builds:
```bash
# Full solution build (Linux/macOS/Windows with Git Bash)
./build.sh --config Debug
./build.sh --config Beta
./build.sh --config Release

# Or using PowerShell (Windows)
./build.ps1 -config Debug
./build.ps1 -config Beta
./build.ps1 -config Release

# Full solution tests
dotnet test Clean.Skd.sln -c Debug --no-build

# Build and test a specific project
dotnet build Clean.Sdk.Domain/Clean.Sdk.Domain.csproj -c Release
dotnet test Clean.Sdk.Domain.Tests/Clean.Sdk.Domain.Tests.csproj -c Release
```
