# 📦 Release NuGet Package - Guía de Uso

## Cómo crear un release

### Opción 1: Ejecutar manualmente (Recomendado)

1. Ve a la pestaña **Actions** en GitHub
2. Selecciona el workflow **"Release NuGet Package"**
3. Haz clic en **"Run workflow"**
4. Ingresa la versión (formato: `1.0.0`, `1.1.0`, `2.0.0`, etc.)
5. Haz clic en **"Run workflow"**

### Opción 2: Crear un tag

```bash
# Crear y pushear un tag
git tag v1.0.0
git push origin v1.0.0
```

## ✅ ¿Qué hace este workflow?

1. **Genera el NuGet Package**
   - ID del paquete: `UPTSiteTests.2021058694` (incluye matrícula)
   - Versión estándar: `1.0.0`, `1.1.0`, etc.
   - Metadata incluye matrícula en InformationalVersion
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
dotnet add package UPTSiteTests.2021058694 --version 1.0.0 --source github
```

## 🔢 Formato de Versión

El paquete usa un formato de versión estándar más el código de matrícula en el ID:

- **Package ID**: `UPTSiteTests.2021058694` (incluye matrícula)
- **Version**: `1.0.0`, `1.1.0`, `2.0.0` (formato estándar)
- **InformationalVersion**: `1.0.0+matricula.2021058694` (metadata completa)

Ejemplos:
- `1.0.0` - Primera versión
- `1.1.0` - Nueva funcionalidad menor
- `2.0.0` - Cambio mayor

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
2. Busca el release `v1.x.x`
3. Descarga el archivo `.nupkg` si lo necesitas

## 📊 Ver el paquete publicado

1. Ve a tu repositorio en GitHub
2. En el menú lateral derecho, busca **Packages**
3. Ahí verás `UPTSiteTests.2021058694` publicado

## 💡 Nota sobre la Matrícula

La matrícula **2021058694** está incluida en:
- ✅ **Package ID**: `UPTSiteTests.2021058694`
- ✅ **Author**: Victor Cruz (2021058694)
- ✅ **InformationalVersion**: `x.x.x+matricula.2021058694`
- ✅ **Description**: Menciona la matrícula
- ✅ **Tags**: Incluye `2021058694`

Esto asegura que la matrícula esté visible en todos los metadatos del paquete NuGet.
