using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Stats.Common.Dto;
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
            //WebBrowser browser = new WebBrowser(logger, appConfig);

            //string html = await browser.LoadUrl("https://www.aba-liga.com/player.php?id=3091");
            //HtmlDocument doc = new HtmlDocument();
            //doc.LoadHtml(html);

            //var playerInfo = new AbaPlayerInfo();

            //logger.LogDebug(playerInfo.ParsePlayerInfo(doc).ToString());

            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(new List<RoundDto>
            {
                new RoundDto(){Id = 1,Season = "2018/19"},
                new RoundDto(){Id = 2,Season = "2018/19"},
                new RoundDto(){Id = 3,Season = "2018/19"}
            });
            var rounds = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://localhost:44325/api/Rounds/PostRounds", rounds);
            response.EnsureSuccessStatusCode();

            return !string.IsNullOrEmpty("done");
        }
    }
}
