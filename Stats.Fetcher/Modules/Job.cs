using System.IO;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stats.Fetcher;
using Stats.Fetcher.Modules;
using Stats.Fetcher.Jobs.ABA;

namespace Stats.Fetcher.Modules
{
    public class Job : IJob
    {
        private readonly ILogger<Job> logger;
        private readonly IOptions<AppConfig> appConfig;

        public Job(ILogger<Job> logger, IOptions<AppConfig> appConfig)
        {
            this.logger = logger;
            this.appConfig = appConfig;
        }

        async Task<bool> IJob.Start()
        {
            WebBrowser browser = new WebBrowser(logger, appConfig);

            string html = await browser.LoadUrl("https://www.aba-liga.com/player.php?id=3091");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var playerInfo = new AbaPlayerInfo();

            logger.LogDebug(playerInfo.ParsePlayerInfo(doc).ToString());

            return !string.IsNullOrEmpty(html);
        }
    }
}
