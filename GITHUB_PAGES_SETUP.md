# Configuración de GitHub Pages

Para que el workflow `publish_cov_report.yml` funcione correctamente y publique los reportes en GitHub Pages, sigue estos pasos:

## Pasos para habilitar GitHub Pages

1. Ve a tu repositorio en GitHub
2. Haz clic en **Settings** (Configuración)
3. En el menú lateral izquierdo, busca y haz clic en **Pages**
4. En la sección **Source** (Fuente), selecciona:
   - **Source**: `GitHub Actions`
5. Guarda los cambios

## ¿Qué hace este workflow?

El workflow automáticamente:

✅ Compila el proyecto .NET
✅ Instala los navegadores de Playwright
✅ Ejecuta todas las pruebas con cobertura de código
✅ Genera el reporte de cobertura en formato HTML
✅ Copia los videos de las pruebas
✅ Copia los traces de Playwright
✅ Crea una página web con enlaces a todos los reportes
✅ Publica todo en GitHub Pages

## Acceder a los reportes

Una vez que el workflow se ejecute exitosamente, podrás acceder a:

- **Página principal**: `https://UPT-FAING-EPIS.github.io/lab-2025-ii-si784-u2-06-cs-Vlkair/`
- **Reporte de cobertura**: `https://UPT-FAING-EPIS.github.io/lab-2025-ii-si784-u2-06-cs-Vlkair/coverage/`
- **Videos**: `https://UPT-FAING-EPIS.github.io/lab-2025-ii-si784-u2-06-cs-Vlkair/videos/`
- **Traces**: `https://UPT-FAING-EPIS.github.io/lab-2025-ii-si784-u2-06-cs-Vlkair/traces/`

## Ejecución del workflow

El workflow se ejecuta automáticamente cuando:
- Haces `push` a la rama `main`
- Creas un Pull Request hacia `main`
- Lo ejecutas manualmente desde la pestaña Actions en GitHub

## Verificar la ejecución

1. Ve a la pestaña **Actions** en tu repositorio
2. Busca el workflow "Publish Coverage Report"
3. Verifica que se ejecute sin errores
4. Una vez completado, los reportes estarán disponibles en GitHub Pages
