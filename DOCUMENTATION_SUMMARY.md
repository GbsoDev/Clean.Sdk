# Clean.Sdk Documentation Summary

This file is the root documentation entry point for the repository.

---

## Primary Documentation (English)

The recommended and actively maintained documentation set is in `docs/`:

1. [docs/README.md](docs/README.md) — Start here
2. [docs/DOCUMENTATION_STANDARD.md](docs/DOCUMENTATION_STANDARD.md) — Standard baseline and compliance mapping
3. [docs/INDEX.md](docs/INDEX.md) — Full navigation map
4. [docs/PROJECT_ANALYSIS.md](docs/PROJECT_ANALYSIS.md) — Technical assessment and roadmap
5. [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) — Architecture description (ISO/IEC/IEEE 42010 aligned)
6. [docs/DEVELOPMENT_GUIDE.md](docs/DEVELOPMENT_GUIDE.md) — Implementation guide for contributors
7. [docs/CONVENTIONS.md](docs/CONVENTIONS.md) — Naming and coding standards
8. [docs/QUICK_REFERENCE.md](docs/QUICK_REFERENCE.md) — Fast daily reference
9. [docs/PROJECT_METRICS.md](docs/PROJECT_METRICS.md) — Measured metrics and KPI recommendations

---

## Current Repository Snapshot

- Runtime: `.NET 8`
- Projects: `8` (`4` production + `4` test)
- C# files (excluding `bin/obj`): `115`
- Estimated C# lines: `4,207`
- Tests: `112/112` passing (evidence date: `2026-02-18`)
- Main architecture: Clean Architecture + CQRS + Repository Pattern

---

## Recommended Reading Order

### New Developers
1. [docs/PROJECT_ANALYSIS.md](docs/PROJECT_ANALYSIS.md)
2. [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md)
3. [docs/QUICK_REFERENCE.md](docs/QUICK_REFERENCE.md)
4. [docs/DEVELOPMENT_GUIDE.md](docs/DEVELOPMENT_GUIDE.md)
5. [docs/CONVENTIONS.md](docs/CONVENTIONS.md)

### Tech Leads / Architects
1. [docs/PROJECT_ANALYSIS.md](docs/PROJECT_ANALYSIS.md)
2. [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md)
3. [docs/PROJECT_METRICS.md](docs/PROJECT_METRICS.md)
4. [docs/CONVENTIONS.md](docs/CONVENTIONS.md)

---

## Notes

- `docs/` is the source of truth for the current English documentation.
- Keep documentation synchronized when introducing architectural or public API changes.
