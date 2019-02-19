using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScrapySharp.Network;
using Stats.Fetcher;
using Stats.Fetcher.Modules;

namespace Stats.Fetcher.Modules
{
    public class WebBrowser : IWebBrowser
    {
        private readonly ILogger<Job> logger;
        private readonly IOptions<AppConfig> appConfig;
        private readonly ScrapingBrowser browser = new ScrapingBrowser();


        public WebBrowser(ILogger<Job> logger, IOptions<AppConfig> appConfig)
        {
            this.logger = logger;
            this.appConfig = appConfig;

            browser.AutoDownloadPagesResources = false;
        }

        public async Task<string> LoadUrl(string url)
        {
            string result = await browser.DownloadStringAsync(new Uri(url));
            logger.LogTrace($"Downloaded {System.Text.Encoding.Unicode.GetByteCount(result)} bytes from {url}");
            return result;
        }
    }
}
