using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace StatsFetcher.modules
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

            string html = await browser.LoadUrl("https://www.aba-liga.com/player.php?id=2589");
            File.WriteAllText("page.html", html);

            return !string.IsNullOrEmpty(html);
        }
    }
}
