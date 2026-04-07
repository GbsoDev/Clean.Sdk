---
name: git-commit-description-editor
description: Gestiona la modificación de mensajes de commits previos y actuales en Git mediante reescritura de historial por cherry-pick.
---

# Git Commit Description Editor

Usa este skill cuando elusuario requiera modificar la descripción o el mensaje de commits que ya han sido creados.

Instrucciones:

## Modificación del Último Commit
Para el commit más reciente, utiliza:
`git commit --amend -m "nuevo mensaje"`
Toma nota de la rama actual.

## Reescritura de Historial (Rango de Commits)
Para modificar un rango de commits partiendo de un `{hash1}` (parámetro de entrada) hasta `{hash2}` (donde `{hash2}` es el `HEAD` o el commit más nuevo del rango):

1. **Identificación**:
   - Analiza los commits desde `{hash1}` hasta `HEAD`.
   - Identifica el hash de `HEAD`.

2. **Proceso de Reescritura**:
   - Listar los hashes en el rango `{hash1}..{hash2}`, asegurándose de incluir `{hash1}` (orden del más antiguo al más nuevo).
   - Crear y cambiar a una rama temporal `rewrite-fix` partiendo del padre de `{hash1}` (`git checkout -b rewrite-fix {hash1}^`).

3. **Iteración y Transformación**:
   Iterar sobre cada hash identificado en el paso 2:
   - Ejecutar `git cherry-pick <hash>`.
   - Obtener el contenido de los cambios mediante `git show <hash>`.
   - Generar un nuevo mensaje basado únicamente en el contenido de los cambios (en inglés y siguiendo **Conventional Commits**).
   - Aplicar el nuevo mensaje con `git commit --amend -m "<mensaje_generado>"`.

4. **Finalización**:
   - Volver a la rama original de la que tomaste nota al inicio.
   - Ejecutar `git reset --hard rewrite-fix` para aplicar la nueva historia.
   - Eliminar la rama `rewrite-fix`.

## Notas Importantes
- **Sin Conflictos**: El proceso debe asegurar que no existan conflictos. Si ocurre un conflicto, el proceso debe ser revisado ya que la lógica de cherry-pick secuencial sobre la base limpia de `{hash1}^` no debería generarlos si se respetan los hashes originales.
- **Orden**: Los commits deben procesarse estrictamente del más antiguo al más nuevo para mantener la integridad de los cambios.
- **Estilo**: Los mensajes deben ser en inglés y cumplir con la convención de Commits Convencionales.

## Verificación
Siempre verifica el resultado final con `git log --oneline` para asegurar que la historia es la esperada.
