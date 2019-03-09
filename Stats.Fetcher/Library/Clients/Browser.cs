using System;
using System.Threading.Tasks;
using ScrapySharp.Network;

namespace Stats.Fetcher.Library.Clients
{
    public class Browser
    {
        private readonly ScrapingBrowser browser = new ScrapingBrowser();


        public Browser()
        {
            browser.AutoDownloadPagesResources = false;
        }

        public async Task<WebPage> GetPage(string url)
        {
            return await browser.NavigateToPageAsync(new Uri(url));
        }
    }
}
