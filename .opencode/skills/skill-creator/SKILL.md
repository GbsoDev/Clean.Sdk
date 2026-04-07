---
name: skill-creator
description: Guía la creación de nuevas skills especializadas para opencode.
---

# Skill Creator

Usa este skill cuando necesites crear una nueva capacidad especializada para el agente, asegurando que siga el formato y las convenciones del sistema.

Instrucciones:

## Proceso de Creación
1. **Definición del Objetivo**: Identifica claramente qué tarea repetitiva o compleja debe resolver la skill.
2. **Estructura de Archivos**: 
   - La skill debe residir en: `.opencode/skills/[nombre-de-la-skill]/SKILL.md`.
   - El nombre de la carpeta debe ser coherente con el nombre de la skill (Kebab-case).
3. **Formato del archivo SKILL.md**:
   - **Frontmatter**: Incluir un bloque YAML al inicio con `name` y `description`.
   - **Título**: Un encabezado H1 con el nombre de la skill.
   - **Disparadores**: Sección que indique cuándo debe activarse el skill (ej. "Usa este skill cuando el usuario pida...").
   - **Instrucciones Detalladas**: Pasos accionables, comandos recomendados, restricciones y flujos de trabajo específicos.

## Convenciones de Diseño
- **Concisión**: Las instrucciones deben ser directas y evitar lenguaje ambiguo.
- **Verificabilidad**: Siempre incluir pasos de verificación (ej. comandos de build o tests) si la skill implica cambios de código.
- **Seguridad**: Si la skill maneja comandos destructivos (como `git push --force` o `rm -rf`), incluir advertencias explícitas y pasos de respaldo.

## Validación de la Skill
Antes de dar por finalizada la creación, verifica que:
- El directorio existe y el archivo `SKILL.md` está correctamente escrito.
- El formato YAML es válido.
- El nombre de la skill en el frontmatter coincide con el nombre la carpeta.
