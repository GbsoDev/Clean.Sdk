# Pipelines CI/CD - Clean.Sdk

## 🌳 Estrategia de Branching: GitFlow Simplificado

### Ramas y Configuraciones

| Rama | Propósito | Build Config | Sufijo NuGet | CI Trigger | CD Trigger | Estabilidad |
|------|-----------|--------------|--------------|------------|------------|-------------|
| **main** | Producción | `Release` | *(ninguno)* | ✅ | ✅ | 🟢 Estable |
| **staging** | Pre-producción/Testing | `Beta` | `-beta` | ✅ | ✅ | 🟡 Testing |
| **develop** | Desarrollo activo | `Debug` | `-dev` | ✅ | ❌ | 🔴 Inestable |

---

## 📋 Reglas de CI/CD

### **Pipeline CI (Integración Continua)**
**Archivo:** `Clean.Sdk-CI.yml`

#### Triggers Automáticos:
- ✅ Push a `main`, `staging`, `develop`
- ✅ Pull Requests a `main`, `staging`, `develop`
- ✅ Cambios en paths: `Clean.Sdk.*/*`

#### Proceso:
1. **Build & Test** de todos los proyectos
2. Orden de ejecución:
   - `Clean.Sdk.Domain` (base)
   - `Clean.Sdk.Data.EfCore` + `Clean.Sdk.Application` (paralelo, dependen de Domain)
   - `Clean.Sdk.Infrastructure` (depende de Application)

#### Build Configuration por Rama:
```yaml
main     → Release (producción optimizada)
staging  → Beta (testing optimizado)
develop  → Debug (con símbolos de depuración)
```

---

### **Pipeline CD (Despliegue Continuo)**
**Archivo:** `Clean.Sdk-CD.yml`

#### Triggers Automáticos:
- ✅ Al completar exitosamente el pipeline CI en `main` o `staging`
- ❌ NO se ejecuta automáticamente para `develop`

#### Proceso:
1. **Obtener versión** desde `Clean.Sdk.Domain.csproj`
2. **Calcular patch version** con counter automático
3. **Build & Pack** de todos los proyectos con versionamiento automático
4. **Publicar** a Azure Artifacts feed: `Gbso.Clean.Sdk/NuGets`

#### Versionamiento:
```
Formato: {major}.{minor}.{patch}{suffix}

Ejemplos:
  main    → 1.2.5       (producción)
  staging → 1.2.5-beta  (testing)
  develop → 1.2.5-dev   (desarrollo)
```

El `patch` se incrementa automáticamente con cada build usando counter.

---

## 🚀 Flujo de Trabajo Recomendado

### 1. Desarrollo Diario (develop)
```bash
git checkout develop
git pull
# hacer cambios
git add .
git commit -m "feat: nueva funcionalidad"
git push origin develop
```
- ✅ CI ejecuta build y tests automáticamente
- ❌ NO genera paquetes NuGet

### 2. Testing/QA (staging)
```bash
git checkout staging
git merge develop
git push origin staging
```
- ✅ CI ejecuta build y tests
- ✅ CD genera paquetes NuGet con sufijo `-beta`
- 📦 Publicados en Azure Artifacts

### 3. Producción (main)
```bash
git checkout main
git merge staging
git push origin main
```
- ✅ CI ejecuta build y tests
- ✅ CD genera paquetes NuGet **SIN sufijo**
- 📦 Publicados en Azure Artifacts (listos para NuGet.org)

---

## 📦 Proyectos y Dependencias

```
Clean.Sdk.Domain (base)
   ↓
├─→ Clean.Sdk.Data.EfCore
├─→ Clean.Sdk.Application
   ↓
└─→ Clean.Sdk.Infrastructure
```

**Orden de Build CD:**
1. Domain (primero, sin dependencias)
2. EfCore + Application (paralelo)
3. Infrastructure (último)

---

## 🔧 Configuración Técnica

### Pool de Ejecución:
- **self-hosted** agents

### Destino de Publicación:
- **Azure Artifacts:** `Gbso.Clean.Sdk/NuGets`
- **NuGet.org:** (configurado, no automático)

### Templates:
- `Clean.Sdk-CI-Template.yml` - Template para jobs de CI
- `Clean.Sdk-CD-Template.yml` - Template para jobs de CD

---

## ⚠️ Importante

1. **NO hacer push directo a `main`** - siempre pasar por `staging` primero
2. **Las versiones se gestionan automáticamente** - NO modificar manualmente
3. **La versión base** se define en `Clean.Sdk.Domain.csproj`
4. **Incrementar versión manualmente** solo cuando hay breaking changes o nuevas features mayores

---

## 🐛 Debugging

### Si CI falla:
- Verificar que todos los tests pasen localmente
- Verificar que no hay errores de compilación
- Revisar logs del pipeline en Azure DevOps

### Si CD falla:
- Verificar que CI completó exitosamente
- Verificar permisos de Azure Artifacts
- Verificar que la versión en Domain.csproj es válida

### Para ejecutar builds locales:
```powershell
# Build completo
./build.ps1

# Build específico
dotnet build Clean.Sdk.Domain/Clean.Sdk.Domain.csproj -c Release
dotnet test Clean.Sdk.Domain.Tests/Clean.Sdk.Domain.Tests.csproj
```
