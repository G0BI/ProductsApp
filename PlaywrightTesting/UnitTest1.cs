using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace PlaywrightTesting
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task PlaywrightTest()
        {
            // create Playwright
            using var playwright = await Playwright.CreateAsync();

            // create browser
            await using var browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 500,
                Timeout = 8000
            });

            // create context
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

            // create page
            var page = await context.NewPageAsync();

            // set viewport to maximum window size

            await page.SetViewportSizeAsync(1920, 1080);

            // open the search engine
            await page.GotoAsync("https://www.google.com/");
            await page.WaitForLoadStateAsync();

            IElementHandle? element = await page.QuerySelectorAsync("text='Reject all'");

            if (element is not null)
            {
                await element.ClickAsync();
                await element.WaitForElementStateAsync(ElementState.Hidden);
                await Task.Delay(TimeSpan.FromSeconds(2.0));
            }

            await page.TypeAsync("[name='q']", ".net core");

            await page.Keyboard.PressAsync("Enter");

            // wait for results to load
            await page.WaitForSelectorAsync("id=appbar");

            // click through to the desired result
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "Screenshots\\Net2.png" });
            await page.ClickAsync("a:has-text(\".NET\")");
            await Task.Delay(TimeSpan.FromSeconds(2.0));
            IElementHandle? button = await page.QuerySelectorAsync("text='Reject'");

            if (button is not null)
            {
                await page.ScreenshotAsync(new PageScreenshotOptions { Path = "Screenshots\\Net3.png" });
                await button.ClickAsync();
                await Task.Delay(TimeSpan.FromSeconds(2.0));
            }

            await Task.Delay(TimeSpan.FromSeconds(2.0));
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "Screenshots\\Net4.png" });
            await page.SetViewportSizeAsync(1920, 1080);

            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "Screenshots\\Net5.png" });
            await page.SetViewportSizeAsync(1920, 1080);
            await Task.Delay(TimeSpan.FromSeconds(5.0));
            var isExist = await page.Locator(selector: "text='Performance'").IsVisibleAsync();

            Assert.That(isExist, Is.False);
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "Screenshots\\Net6.png" });
            await page.SetViewportSizeAsync(1920, 1080);

            await Task.Delay(TimeSpan.FromSeconds(5.0));
            await context.CloseAsync();
        }
    }
}