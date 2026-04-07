---
name: git-commit-description-editor
description: Gestiona la modificación de mensajes de commits previos y actuales en Git.
---

# Git Commit Description Editor

Usa este skill cuando el usuario requiera modificar la descripción o el mensaje de commits que ya han sido creados.

Instrucciones:

## Modificación del Último Commit
Para el commit más reciente, utiliza:
`git commit --amend -m "nuevo mensaje"`

## Modificación de Commits Previos (No remotos)
Cuando el commit no es el último y no ha sido subido al servidor remoto:
1. Identifica la historia de commits con `git log`.
2. Realiza un rebase interactivo. Dado que el entorno de CLI es no interactivo, se debe automatizar mediante la configuración de variables de entorno:
   - `GIT_SEQUENCE_EDITOR`: Para cambiar `pick` por `reword` en el commit objetivo mediante `sed`.
   - `GIT_EDITOR`: Para inyectar el nuevo mensaje automáticamente redirigiendo el contenido al archivo temporal de Git.
3. Ejecuta `git rebase -i [hash_padre]`.

## Metodología Alternativa (Sustitución Manual)
Si el rebase automatizado falla o es demasiado complejo:
1. `git reset --soft [hash_padre]` para mover el HEAD manteniendo los cambios en el área de stage.
2. Guardar cambios temporales con `git stash` si es necesario.
3. Re-aplicar los commits deseados utilizando `git cherry-pick -n [hash]` seguido de `git commit -m "nuevo mensaje"` para cada commit a modificar.

## Verificación
Siempre verifica el resultado final con `git log --oneline` para asegurar que la historia es la esperada.
