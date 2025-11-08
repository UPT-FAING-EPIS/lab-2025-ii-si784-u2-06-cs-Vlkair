# 📦 Release NuGet Package - Guía de Uso

## Cómo crear un release

### Opción 1: Ejecutar manualmente (Recomendado)

1. Ve a la pestaña **Actions** en GitHub
2. Selecciona el workflow **"Release NuGet Package"**
3. Haz clic en **"Run workflow"**
4. Ingresa la versión (formato: `2021058694.1.0`, `2021058694.2.0`, etc.)
5. Haz clic en **"Run workflow"**

### Opción 2: Crear un tag

```bash
# Crear y pushear un tag
git tag v2021058694.1.0
git push origin v2021058694.1.0
```

## ✅ ¿Qué hace este workflow?

1. **Genera el NuGet Package**
   - Versión basada en código de matrícula: `2021058694.x.x`
   - Incluye todos los tests automatizados
   - Metadata completa del paquete

2. **Publica en GitHub Packages**
   - Disponible en: `https://nuget.pkg.github.com/UPT-FAING-EPIS/index.json`
   - Accesible para todo el equipo

3. **Crea el Release en GitHub**
   - Genera un release automático
   - Adjunta el archivo `.nupkg`
   - Incluye notas de la versión

## 📥 Cómo usar el paquete NuGet

### Configurar la fuente

```bash
# Agregar GitHub Packages como fuente
dotnet nuget add source https://nuget.pkg.github.com/UPT-FAING-EPIS/index.json \
  --name github \
  --username TU_USUARIO \
  --password TU_GITHUB_TOKEN
```

### Instalar el paquete

```bash
dotnet add package UPTSiteTests --version 2021058694.1.0 --source github
```

## 🔢 Formato de Versión

El formato de versión usa tu código de matrícula:

```
2021058694.MAJOR.MINOR
```

Ejemplos:
- `2021058694.1.0` - Primera versión
- `2021058694.1.1` - Fix menor
- `2021058694.2.0` - Nueva funcionalidad

## 📋 Contenido del Paquete

- ✅ 5 Tests automatizados con Playwright
- ✅ Configuración de grabación de video
- ✅ Configuración de traces para debugging
- ✅ Soporte para cobertura de código

## 🔐 Permisos necesarios

El workflow necesita:
- ✅ `contents: write` - Para crear releases
- ✅ `packages: write` - Para publicar en GitHub Packages

Estos permisos ya están configurados en el workflow.

## 🎯 Verificar el release

1. Ve a la pestaña **Releases** en tu repositorio
2. Busca el release `v2021058694.x.x`
3. Descarga el archivo `.nupkg` si lo necesitas

## 📊 Ver el paquete publicado

1. Ve a tu repositorio en GitHub
2. En el menú lateral derecho, busca **Packages**
3. Ahí verás `UPTSiteTests` publicado
