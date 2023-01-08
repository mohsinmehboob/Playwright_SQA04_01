using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace Playwright_SQA04_01
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 50 });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.FillAsync("#username", "AmirTester");
            await page.FillAsync("#password", "AmirTester");
            await page.ClickAsync("#login");

            await page.CloseAsync();
        }

        [TestMethod]
        [TestCategory("Login")]
        public async Task TestMethod_SaveVideo()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 1, });

            var context = await browser.NewContextAsync(new()
            {
                RecordVideoDir = "videos/"
            });

            var page = await context.NewPageAsync();
            await page.SetViewportSizeAsync(1920, 1080);

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.FillAsync("#username", "AmirTester");
            await page.FillAsync("#password", "AmirTester");
            await page.ClickAsync("#login");

            await context.CloseAsync();
            await browser.CloseAsync();
        }

        [TestMethod]
        [TestCategory("Login")]
        public async Task TestMethod_SaveState()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 1, });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.FillAsync("#username", "AmirTester");
            await page.FillAsync("#password", "AmirTester");
            await page.ClickAsync("#login");

            // Save storage state into the file.
            await context.StorageStateAsync(new()
            {
                Path = @"c:\state_sqa04.json"
            });
            await context.CloseAsync();
            await browser.CloseAsync();
        }

        [TestMethod]
        [TestCategory("Login")]
        public async Task TestMethod_RetrieveState()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 1, });

            //  Create a new context with the saved storage state.
            var context = await browser.NewContextAsync(new()
            {
                StorageStatePath = @"c:\state_sqa04.json",
            });

            var page = await context.NewPageAsync();

            Thread.Sleep(5000);

            // await page.GotoAsync("https://adactinhotelapp.com/SearchHotel.php");
            // await page.TypeAsync("#location", "Sydney");           

            await page.GotoAsync("https://adactinhotelapp.com/SelectHotel.php");
            await page.ClickAsync("#continue");
        }

        [TestMethod]
        [TestCategory("Login")]
        public async Task TestMethod_Trace()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            { Headless = false, SlowMo = 1, });

            var context = await browser.NewContextAsync();

            // Start tracing before creating / navigating a page.
            await context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

            var page = await context.NewPageAsync();
            await page.SetViewportSizeAsync(1920, 1080);

            await page.GotoAsync("https://adactinhotelapp.com/");
            await page.FillAsync("#username", "AmirTester");
            await page.FillAsync("#password", "AmirTester");
            await page.ClickAsync("#login");

            // Stop tracing and export it into a zip archive.
            await context.Tracing.StopAsync(new()
            {
                Path = "trace.zip"
            });

            await context.CloseAsync();
            await browser.CloseAsync();
        }
    }
}