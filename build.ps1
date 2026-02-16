#!/usr/bin/env pwsh
param(
    [Parameter(Mandatory=$true)]
    [ValidateSet("Beta", "Release")]
    [Alias("c")]
    [string]$config
)

$ErrorActionPreference = "Stop"

$projects = @(
    "Clean.Sdk.Domain",
    "Clean.Sdk.Application",
    "Clean.Sdk.Data.EfCore",
    "Clean.Sdk.Infrastructure"
)

Write-Host "Starting build process for configuration: $config" -ForegroundColor Cyan
Write-Host ""

foreach ($project in $projects) {
    Write-Host "Building $project..." -ForegroundColor Yellow
    
    $projectPath = Join-Path $PSScriptRoot "$project\$project.csproj"
    
    if (-not (Test-Path $projectPath)) {
        Write-Host "ERROR: Project file not found: $projectPath" -ForegroundColor Red
        exit 1
    }
    
    dotnet build $projectPath --configuration $config
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "ERROR: Build failed for $project" -ForegroundColor Red
        exit $LASTEXITCODE
    }
    
    Write-Host "Successfully built $project" -ForegroundColor Green
    Write-Host ""
}

Write-Host "All projects built successfully!" -ForegroundColor Green
exit 0
