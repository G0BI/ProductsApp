using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Threading.Tasks;

namespace PlaywrightCodegen
{
    [TestFixture]
    public class Tests : PageTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task MyTest()
        {
            using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 500,
                Timeout = 8000
            });
            var context = await browser.NewContextAsync(new BrowserNewContextOptions()
            {
                RecordVideoDir = "video\\",
                RecordVideoSize = new RecordVideoSize()
                {
                    Width = 1920,
                    Height = 1080
                },
                ViewportSize = new ViewportSize()
                {
                    Width = 1920,
                    Height = 1080
                }
            });

            var page = await context.NewPageAsync();
            await page.GotoAsync("http://localhost:55402/Product/Index");
            await page.GetByRole(AriaRole.Link, new() { Name = "Create New" }).ClickAsync();
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "Screenshots\\Fruit1.png" });

            await page.GetByLabel("Product Name").ClickAsync();
            await page.GetByLabel("Product Name").FillAsync("Plums");
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "Screenshots\\Fruit2.png" });

            await page.GetByLabel("Price").ClickAsync();
            await page.GetByLabel("Price").FillAsync("0.59");
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "Screenshots\\Fruit3.png" });

            await page.GetByLabel("description").ClickAsync();
            await page.GetByLabel("description").FillAsync("Succulent and juicy plums");
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "Screenshots\\Fruit4.png" });

            await page.GetByRole(AriaRole.Button, new() { Name = "Create" }).ClickAsync();
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "Screenshots\\Fruit5.png" });

        }
    }
}