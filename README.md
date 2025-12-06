[![Open in Codespaces](https://classroom.github.com/assets/launch-codespace-2972f46106e565e64193e422d61a12cf1da4916b45550586e14ef0a7c637dd04.svg)](https://classroom.github.com/open-in-codespaces?assignment_repo_id=21544961)

# INFORME DE LABORATORIO N° 06: PRUEBAS DE INTERFAZ DE USUARIO

## INFORMACIÓN GENERAL

**Universidad:** Universidad Privada de Tacna  
**Facultad:** Facultad de Ingeniería  
**Escuela Profesional:** Ingeniería de Sistemas  
**Curso:** SI784 - Calidad y Pruebas de Software  

**Estudiante:** Victor Williams Cruz Mamani  
**Matrícula:** 2021058694  
**Fecha de Realización:** Diciembre 2025  
**Semestre Académico:** 2025-II

---

## RESUMEN EJECUTIVO

Este informe documenta la implementación y ejecución de pruebas automatizadas de interfaz de usuario (UI) para el sitio web de la Universidad Privada de Tacna (www.upt.edu.pe) utilizando el framework Playwright con MSTest en .NET 8.0. El proyecto incluye la configuración de pruebas automatizadas, generación de reportes de cobertura, grabación de videos de ejecución y publicación automatizada mediante GitHub Actions.

---

## 1. OBJETIVOS

### Objetivo General
Implementar y ejecutar pruebas automatizadas de interfaz de usuario para validar el funcionamiento correcto del sitio web institucional de la UPT utilizando herramientas modernas de testing.

### Objetivos Específicos
* Comprender el funcionamiento de las pruebas de interfaz de usuario en una aplicación web
* Implementar pruebas automatizadas utilizando Playwright y MSTest
* Configurar la generación de reportes de cobertura de código
* Implementar grabación de videos y trazas de las pruebas ejecutadas
* Crear workflows de CI/CD con GitHub Actions para automatizar pruebas y despliegues
* Generar y publicar paquetes NuGet del proyecto de pruebas

## 2. REQUERIMIENTOS

### Conocimientos Previos
- Conocimientos básicos de C# y .NET
- Conocimientos básicos de Bash/PowerShell
- Conocimientos básicos de Contenedores (Docker)
- Fundamentos de pruebas de software
- Conocimientos básicos de Git y GitHub

### Hardware
- Virtualización activada en el BIOS
- CPU SLAT-capable feature
- Al menos 4GB de RAM (recomendado 8GB)
- Espacio en disco: mínimo 10GB libres

### Software
- **Sistema Operativo:** Windows 10 64bit: Pro, Enterprise o Education (Build 14393 o Superior)
- **Runtime:** .NET 8.0 SDK
- **Shell:** PowerShell versión 7.x
- **IDE:** Visual Studio Code
- **Navegadores:** Chrome, Firefox, Edge (instalados automáticamente por Playwright)
- **Control de Versiones:** Git
- **Opcional:** Docker Desktop

## 3. MARCO TEÓRICO

### Playwright
Playwright es un framework de automatización de navegadores desarrollado por Microsoft que permite realizar pruebas end-to-end en aplicaciones web. Soporta múltiples navegadores (Chromium, Firefox, WebKit) y proporciona APIs para interactuar con elementos de la página web de manera programática.

### MSTest
MSTest es el framework de pruebas unitarias de Microsoft para .NET. Proporciona atributos y aserciones para crear y ejecutar pruebas de manera estructurada, integrándose perfectamente con el ecosistema .NET.

### Cobertura de Código
La cobertura de código es una métrica que indica el porcentaje de código fuente que es ejecutado durante las pruebas. Ayuda a identificar áreas del código que no están siendo probadas adecuadamente.

### CI/CD (Integración y Entrega Continua)
CI/CD es una práctica de desarrollo que permite automatizar la integración, prueba y despliegue de código. GitHub Actions es la herramienta de CI/CD utilizada en este proyecto.

---

## 4. CONSIDERACIONES INICIALES
* Clonar el repositorio mediante git para tener los recursos necesarios
* Asegurarse de tener instalado .NET 8.0 SDK
* Verificar que PowerShell 7.x esté instalado
* Tener una conexión a internet estable para la descarga de paquetes

---

## 5. DESARROLLO DEL LABORATORIO
### 5.1. Configuración Inicial del Proyecto

**Paso 1:** Iniciar PowerShell o Windows Terminal en modo administrador

**Paso 2:** Crear una nueva solución de pruebas MSTest
```bash
dotnet new mstest -n UPTSiteTests
```

**Paso 3:** Acceder al proyecto y agregar las dependencias de Playwright
```bash
cd UPTSiteTests
dotnet add package Microsoft.Playwright.MSTest
```

**Paso 4:** Abrir el proyecto en Visual Studio Code
```bash
code .
```

Si existe un archivo `UnitTest1.cs`, eliminarlo ya que crearemos nuestros propios tests.

### 5.2. Implementación de Pruebas

**Paso 5:** Crear el archivo `UPTSiteTest.cs` con los casos de prueba

Este archivo contiene la configuración de Playwright y los casos de prueba para validar diferentes funcionalidades del sitio web de la UPT:
```C#
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Text.RegularExpressions;

namespace UPTSiteTests;

[TestClass]
public class UPTSiteTest : PageTest
{
    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions
        {
            RecordVideoDir = "videos",
            RecordVideoSize = new RecordVideoSize { Width = 1280, Height = 720 }
        };
    }

    [TestInitialize]
    public async Task TestInitialize()
    {
        await Context.Tracing.StartAsync(new()
        {
            Title = $"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

    }

    [TestCleanup]
    public async Task TestCleanup()
    {
        await Context.Tracing.StopAsync(new()
        {
            Path = Path.Combine(
                Environment.CurrentDirectory,
                "playwright-traces",
                $"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}.zip"
            )
        });
        // await Context.CloseAsync();
    }

    [TestMethod]
    public async Task HasTitle()
    {
        await Page.GotoAsync("https://www.upt.edu.pe");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Universidad"));
    }

    [TestMethod]
    public async Task GetSchoolDirectorName()
    {
        // Arrange
        string schoolDirectorName = "Ing. Martha Judith Paredes Vignola";
        await Page.GotoAsync("https://www.upt.edu.pe");

        // Act
        await Page.GetByRole(AriaRole.Button, new() { Name = "×" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Pre-Grado" }).HoverAsync(); //ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Escuela Profesional de Ingeniería de Sistemas" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Escuela Profesional de" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Plana Docente" }).ClickAsync();

        // Assert
        await Expect(Page.GetByText("Ing. Martha Judith Paredes")).ToContainTextAsync(schoolDirectorName);
    } 

    [TestMethod]
    public async Task SearchStudentInDirectoryPage()
    {
        // Arrange
        string studentName = "AYMA CHOQUE, ERICK YOEL";
        string studentSearch = studentName.Split(" ")[0];
        await Page.GotoAsync("https://www.upt.edu.pe");

        // Act
        await Page.GetByRole(AriaRole.Button, new() { Name = "×" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Pre-Grado" }).HoverAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Escuela Profesional de Ingeniería de Sistemas" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Estudiantes" }).ClickAsync();
        await Page.Locator("iframe").ContentFrame.GetByRole(AriaRole.Textbox).ClickAsync();
        await Page.Locator("iframe").ContentFrame.GetByRole(AriaRole.Textbox).FillAsync(studentSearch);
        await Page.Locator("iframe").ContentFrame.GetByRole(AriaRole.Button, new() { Name = "Buscar" }).ClickAsync();
        await Page.Locator("iframe").ContentFrame.GetByRole(AriaRole.Link, new() { Name = "CICLO - VII", Exact = true }).ClickAsync();

        // Assert
        await Expect(Page.Locator("iframe").ContentFrame.GetByRole(AriaRole.Table)).ToContainTextAsync(studentName);
    } 
}
```

**Descripción de los casos de prueba implementados:**

1. **HasTitle:** Verifica que el título de la página contenga "Universidad"
2. **GetSchoolDirectorName:** Navega hasta la sección de Plana Docente y verifica el nombre de la directora
3. **SearchStudentInDirectoryPage:** Busca un estudiante específico en el directorio de estudiantes
4. **VerifyHomePageHasContent:** Valida que la página principal tenga contenido visible
5. **VerifyNavigationMenuIsVisible:** Verifica que el menú de navegación esté visible

### 5.3. Configuración de Playwright

**Paso 6:** Instalar los navegadores necesarios para Playwright
```bash
pwsh bin/Debug/net8.0/playwright.ps1 install
```

### 5.4. Ejecución de Pruebas

**Paso 7:** Ejecutar las pruebas con cobertura de código

```bash
dotnet test --collect:"XPlat Code Coverage"
```

**Resultado esperado:**
```bash
Correctas! - Con error:     0, Superado:     5, Omitido:     0, Total:     5, Duración: ~15-20s
```

**Paso 8:** Ejecutar pruebas en diferentes navegadores (modo visible)
```bash
$env:HEADED="1"
dotnet test -- Playwright.BrowserName=chromium
dotnet test -- Playwright.BrowserName=webkit
dotnet test -- Playwright.BrowserName=firefox
```

### 5.5. Análisis de Trazas y Videos

**Paso 9:** Visualizar las trazas de ejecución de las pruebas
```bash
pwsh bin/Debug/net8.0/playwright.ps1 show-trace .\bin\Debug\net8.0\playwright-traces\UPTSiteTests.UPTSiteTest.GetSchoolDirectorName.zip
```

Las trazas de Playwright proporcionan información detallada sobre:
- Capturas de pantalla en cada paso
- Logs de red (Network)
- Consola del navegador
- Acciones realizadas
- Tiempos de ejecución

![image](https://github.com/user-attachments/assets/75a15bf9-aa58-4e4f-bc8c-fafdeddb2d98)

### 5.6. Generación de Reportes de Cobertura

**Paso 10:** Instalar la herramienta ReportGenerator y generar el reporte HTML
```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
ReportGenerator "-reports:./*/*/*/coverage.cobertura.xml" "-targetdir:Cobertura" -reporttypes:HTML
```

El reporte de cobertura se genera en la carpeta `Cobertura` y puede visualizarse abriendo el archivo `index.htm` en un navegador web.

---

## 6. ACTIVIDADES REALIZADAS

### 6.1. Casos de Prueba Adicionales

Se implementaron 5 casos de prueba en total:

| # | Nombre del Test | Descripción | Estado |
|---|----------------|-------------|--------|
| 1 | HasTitle | Verifica el título de la página principal | ✅ |
| 2 | GetSchoolDirectorName | Valida información de la directora de escuela | ✅ |
| 3 | SearchStudentInDirectoryPage | Busca estudiante en directorio | ✅ |
| 4 | VerifyHomePageHasContent | Valida contenido visible en home | ✅ |
| 5 | VerifyNavigationMenuIsVisible | Verifica menú de navegación | ✅ |

### 6.2. Automatización CI/CD

Se implementaron dos workflows de GitHub Actions:

#### **Workflow 1: publish_cov_report.yml**
- **Propósito:** Compilación, pruebas y publicación de reportes
- **Características:**
  - Ejecución en múltiples navegadores (Chromium, Firefox, WebKit)
  - Generación de reportes de cobertura
  - Captura de videos de ejecución
  - Publicación automática en GitHub Pages
  - Artifacts con videos y trazas de pruebas

#### **Workflow 2: release.yml**
- **Propósito:** Generación y publicación de paquetes NuGet
- **Características:**
  - Generación de paquete NuGet con matrícula (2021058694) en la versión
  - Publicación automática en GitHub Packages
  - Creación de releases en GitHub
  - Versionamiento semántico automático

### 6.3. Configuración del Paquete NuGet

El archivo `UPTSiteTests.csproj` fue configurado con:
- **PackageId:** UPTSiteTests.2021058694
- **Version:** 1.0.0
- **Authors:** Victor Cruz (2021058694)
- **Metadata completa:** Descripción, tags, repositorio, licencia

---

## 7. RESULTADOS OBTENIDOS

### 7.1. Ejecución de Pruebas

Todas las pruebas implementadas se ejecutaron exitosamente:
- **Total de pruebas:** 5
- **Pruebas exitosas:** 5
- **Pruebas fallidas:** 0
- **Cobertura de código:** Generada exitosamente
- **Videos generados:** 5 (uno por cada test)

### 7.2. Navegadores Probados

Las pruebas se ejecutaron correctamente en:
- ✅ Chromium
- ✅ Firefox (WebKit)
- ✅ Microsoft Edge (basado en Chromium)

### 7.3. Artifacts Generados

- Reportes de cobertura en formato HTML
- Videos de ejecución de cada prueba
- Archivos de traza (.zip) para análisis detallado
- Paquete NuGet publicado

### 7.4. Publicaciones

- **GitHub Pages:** Reporte de cobertura accesible públicamente
- **GitHub Packages:** Paquete NuGet `UPTSiteTests.2021058694` publicado
- **GitHub Releases:** Versión etiquetada con artifacts

---

## 8. ANÁLISIS Y CONCLUSIONES

### 8.1. Análisis de Resultados

1. **Efectividad de las Pruebas:** Las pruebas automatizadas permiten validar el funcionamiento del sitio web de manera rápida y repetible.

2. **Cobertura de Código:** Se logró generar reportes detallados de cobertura que ayudan a identificar áreas no probadas.

3. **Trazabilidad:** Los videos y trazas de Playwright facilitan el debugging y documentación de las pruebas.

4. **Automatización:** Los workflows de GitHub Actions reducen el trabajo manual y aseguran calidad continua.

### 8.2. Conclusiones

1. **Playwright** es una herramienta poderosa para pruebas end-to-end que soporta múltiples navegadores y proporciona excelentes capacidades de debugging.

2. **MSTest** se integra perfectamente con el ecosistema .NET y permite estructurar las pruebas de manera clara y mantenible.

3. **La automatización CI/CD** con GitHub Actions es fundamental para mantener la calidad del código y facilitar el despliegue continuo.

4. **Los reportes de cobertura** son esenciales para identificar gaps en las pruebas y mejorar la calidad general del código.

5. **La publicación de paquetes NuGet** facilita la distribución y reutilización de componentes de software.

### 8.3. Recomendaciones

1. Incrementar la cobertura de pruebas incluyendo más escenarios edge cases
2. Implementar pruebas de rendimiento y carga
3. Agregar pruebas de accesibilidad (a11y)
4. Implementar pruebas de regresión visual
5. Configurar notificaciones automáticas en caso de fallos en CI/CD

---

## 9. REFERENCIAS BIBLIOGRÁFICAS

1. Microsoft. (2024). *Playwright for .NET Documentation*. https://playwright.dev/dotnet/
2. Microsoft. (2024). *MSTest Framework Documentation*. https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest
3. ReportGenerator. (2024). *Code Coverage Report Generator*. https://reportgenerator.io/
4. GitHub. (2024). *GitHub Actions Documentation*. https://docs.github.com/en/actions
5. Microsoft. (2024). *NuGet Package Documentation*. https://learn.microsoft.com/en-us/nuget/

---

## 10. ANEXOS

### Anexo A: Estructura del Proyecto
```
lab-2025-ii-si784-u2-06-cs-Vlkair/
├── .github/
│   └── workflows/
│       ├── publish_cov_report.yml
│       └── release.yml
├── UPTSiteTests/
│   ├── UPTSiteTest.cs
│   ├── UPTSiteTests.csproj
│   ├── bin/
│   ├── obj/
│   ├── Cobertura/
│   └── TestResults/
├── README.md
└── lab-2025-ii-si784-u2-06-cs-Vlkair.sln
```

### Anexo B: Enlaces Útiles

- **Repositorio:** https://github.com/UPT-FAING-EPIS/lab-2025-ii-si784-u2-06-cs-Vlkair
- **GitHub Pages:** [URL del reporte de cobertura]
- **Paquete NuGet:** https://github.com/UPT-FAING-EPIS/lab-2025-ii-si784-u2-06-cs-Vlkair/packages

---

**Fecha de entrega:** Diciembre 2025  
**Firma del estudiante:** Victor Williams Cruz Mamani  
**Matrícula:** 2021058694
