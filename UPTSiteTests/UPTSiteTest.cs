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
        await Page.GotoAsync("https://www.upt.edu.pe", new() { Timeout = 60000 });

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Universidad"));
    }

    [TestMethod]
    public async Task GetSchoolDirectorName()
    {
        // Arrange
        string schoolDirectorName = "Ing. Martha Judith Paredes Vignola";
        await Page.GotoAsync("https://www.upt.edu.pe", new() { Timeout = 60000 });

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
        string studentName = "CRUZ MAMANI, VICTOR WILLIAMS";
        string studentSearch = "CRUZ";
        await Page.GotoAsync("https://www.upt.edu.pe", new() { Timeout = 60000 });

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

    [TestMethod]
    public async Task VerifyHomePageHasContent()
    {
        // Arrange
        await Page.GotoAsync("https://www.upt.edu.pe", new() { Timeout = 60000 });

        // Act
        await Page.GetByRole(AriaRole.Button, new() { Name = "×" }).ClickAsync();

        // Assert - Verificar que la página tiene contenido visible
        await Expect(Page.Locator("body")).ToBeVisibleAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("upt.edu.pe"));
    }

    [TestMethod]
    public async Task VerifyNavigationMenuIsVisible()
    {
        // Arrange
        await Page.GotoAsync("https://www.upt.edu.pe", new() { Timeout = 60000 });

        // Act
        await Page.GetByRole(AriaRole.Button, new() { Name = "×" }).ClickAsync();
        
        // Assert - Verificar que el menú de navegación principal está visible
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Pre-Grado" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Post-Grado" })).ToBeVisibleAsync();
    }
}