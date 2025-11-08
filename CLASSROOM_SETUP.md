# Instrucciones para GitHub Classroom

## Problema resuelto ✅

El workflow de GitHub Classroom (`classroom.yml`) ahora puede ejecutar los tests correctamente utilizando el script `setup-tests.sh`.

## Cómo funciona

El archivo `classroom.yml` debe usar este comando en el `setup-command`:

```yaml
setup-command: chmod +x setup-tests.sh && ./setup-tests.sh
```

Y en el `command`:

```yaml
command: cd UPTSiteTests && dotnet test --no-build
```

## ¿Qué hace el script setup-tests.sh?

1. ✅ Restaura las dependencias de .NET
2. ✅ Compila el proyecto
3. ✅ Instala los navegadores de Playwright con todas las dependencias
4. ✅ Prepara el entorno para ejecutar los tests

## Ejemplo de configuración en classroom.yml

```yaml
- name: t1
  id: t1
  uses: classroom-resources/autograding-command-grader@v1
  with:
    test-name: t1
    setup-command: chmod +x setup-tests.sh && ./setup-tests.sh
    command: cd UPTSiteTests && dotnet test --no-build
    timeout: 20
    max-score: 2
```

## Ventajas de esta solución

- ✅ No modifica el archivo `classroom.yml` original
- ✅ Reutilizable en múltiples tests
- ✅ Fácil de mantener y actualizar
- ✅ Compatible con CI/CD
- ✅ Instala automáticamente todas las dependencias necesarias
