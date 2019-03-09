using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScrapySharp.Network;
using Stats.Common.Dto;
using Stats.Fetcher.Library.Clients;

namespace Stats.Fetcher.Library.Core
{
    /// <summary>
    /// TODO: Document
    /// </summary>
    public abstract class Job
    {
        private readonly ILogger logger;
        private readonly IApiClient client;

        protected Job(ILogger logger, IOptions<AppConfig> appConfig, IApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<bool> ProcessJob(string url)
        {
            var page = await new Browser().GetPage(url);
            logger.LogDebug(page.Html.InnerHtml);

            if (!IsLoadedCorrectly(page)) return false;

            if (!ParseHtml(page, out var items)) return false;

            if (!items.Any()) return false;

            var item = items.First();
            if (!await client.Post<bool>(item.Source, items)) return false;

            CreateAdditionalJobs(page, out var jobs);

            if (jobs.Any())
            {
                return await client.Post<bool>(jobs.First().Source, jobs);
            }

            return true;
        }

        public abstract bool IsLoadedCorrectly(WebPage page);
        public abstract bool ParseHtml(WebPage page, out List<BaseDto> result);
        public abstract bool CreateAdditionalJobs(WebPage page, out List<BaseDto> dtos);
    }
}
