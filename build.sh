#!/bin/bash
set -e

CONFIG=""

while [[ $# -gt 0 ]]; do
    case $1 in
        --config|-c)
            CONFIG="$2"
            shift 2
            ;;
        *)
            echo "Unknown parameter: $1"
            exit 1
            ;;
    esac
done

if [[ -z "$CONFIG" ]]; then
    echo "ERROR: --config parameter is required (Beta or Release)"
    exit 1
fi

if [[ "$CONFIG" != "Beta" && "$CONFIG" != "Release" ]]; then
    echo "ERROR: config must be Beta or Release"
    exit 1
fi

PROJECTS=(
    "Clean.Sdk.Domain"
    "Clean.Sdk.Application"
    "Clean.Sdk.Data"
    "Clean.Sdk.Data.EfCore"
    "Clean.Sdk.Infrastructure"
)

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "Starting build process for configuration: $CONFIG"
echo ""

for PROJECT in "${PROJECTS[@]}"; do
    echo "Building $PROJECT..."
    
    PROJECT_PATH="$SCRIPT_DIR/$PROJECT/$PROJECT.csproj"
    
    if [[ ! -f "$PROJECT_PATH" ]]; then
        echo "ERROR: Project file not found: $PROJECT_PATH"
        exit 1
    fi
    
    dotnet build "$PROJECT_PATH" --configuration "$CONFIG"
    
    if [[ $? -ne 0 ]]; then
        echo "ERROR: Build failed for $PROJECT"
        exit 1
    fi
    
    echo "Successfully built $PROJECT"
    echo ""
done

echo "All projects built successfully!"
exit 0
