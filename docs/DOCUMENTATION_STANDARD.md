# Documentation Standard and Compliance Mapping

## Purpose

This document defines the documentation baseline for `Clean.Sdk` and maps the current documentation set to internationally recognized standards.

## Selected Standards

The project documentation is aligned to the following standards:

1. **ISO/IEC/IEEE 42010** — Architecture description for software-intensive systems.
2. **ISO/IEC/IEEE 26514** — Requirements for user documentation in software lifecycle processes.
3. **ISO/IEC/IEEE 26515** — Developing information for users in an agile environment.

These standards are widely adopted for architecture communication, documentation quality, and maintainability in professional engineering organizations.

## Scope of Alignment

This alignment applies to repository-level technical documentation in `docs/` and supporting root documentation entries.

It does **not** claim formal certification. It provides an operational, auditable structure based on standard principles.

## Mapping by Document

| Document | Main Objective | Primary Standard Mapping |
|---|---|---|
| `ARCHITECTURE.md` | Stakeholders, concerns, viewpoints, rationale, constraints | ISO/IEC/IEEE 42010 |
| `PROJECT_ANALYSIS.md` | Current-state assessment, risks, roadmap, quality status | ISO/IEC/IEEE 26514 / 26515 |
| `DEVELOPMENT_GUIDE.md` | Implementation workflows and contributor procedures | ISO/IEC/IEEE 26515 |
| `CONVENTIONS.md` | Shared engineering language and coding standards | ISO/IEC/IEEE 26514 |
| `PROJECT_METRICS.md` | Objective measurements and KPI governance | ISO/IEC/IEEE 26514 |
| `QUICK_REFERENCE.md` | Task-oriented operational reference | ISO/IEC/IEEE 26515 |
| `INDEX.md` / `README.md` | Navigation, audience paths, usage context | ISO/IEC/IEEE 26514 |

## Mandatory Quality Criteria

Every documentation update must satisfy the following:

- **Accuracy**: values and statements are evidence-based and traceable to code/configuration/CI.
- **Consistency**: no contradictory metrics across `README.md`, `docs/`, and summary files.
- **Audience fitness**: explicit audience segmentation (new contributors, architects, maintainers).
- **Maintainability**: each document includes clear ownership intent and update triggers.
- **Traceability**: architecture, metrics, and roadmap entries are connected to concrete repository artifacts.

## Documentation Governance

### Update Triggers

Documentation updates are required when any of the following changes:

- Public API behavior
- Dependency model between layers/projects
- Build/release mechanics
- Supported runtime/framework versions
- Test strategy or quality gates
- Packaging/distribution flow

### Update Workflow

1. Update source document(s) in `docs/`.
2. Update cross-references in `docs/INDEX.md` and root `DOCUMENTATION_SUMMARY.md` if needed.
3. Verify measurable values (`PROJECT_METRICS.md`, root `README.md`) against current repository state.
4. Include documentation update confirmation in pull request checklist.

## Review Cadence

- **Per change**: mandatory for architecture/public behavior changes.
- **Monthly**: metrics and quality status refresh.
- **Quarterly**: roadmap and architecture concern review.

## Evidence Date

Current baseline evidence date: **2026-04-07**.