using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Text.RegularExpressions;

namespace Playwright_SQA04_01
{
    [TestClass]
    public class PageTestExec : PageTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            await Page.GotoAsync("https://adactinhotelapp.com/index.php");
            await Expect(Page).ToHaveTitleAsync(new Regex("Adactin.com"));
            await Page.FillAsync("#username", "AmirTester");
            await Page.FillAsync("#password", "AmirTester");
            await Page.ClickAsync("#login");

            await Expect(Page).ToHaveTitleAsync(new Regex("Adactin.com - Search Hotel"));

        }

        [TestMethod]
        public async Task TestMethod2()
        {
            await Page.GotoAsync("https://adactinhotelapp.com/index.php");
            await Expect(Page).ToHaveTitleAsync(new Regex("Adactin.com"));
        }
    }
}