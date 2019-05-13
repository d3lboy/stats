using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScrapySharp.Network;
using Stats.Common.Dto;
using Stats.Common.Enums;
using Stats.Fetcher.Library.Clients;
using Stats.Fetcher.Library.Core;

namespace Stats.Fetcher.Jobs.ABA
{
    [JobFlags(Common.Enums.Competition.Aba, JobType.Game)]
    public class Game: JobBase
    {
        private readonly ILogger<Game> logger;

        public Game(ILogger<Game> logger, IOptions<AppConfig> appConfig, IApiClient client) : base(logger, client)
        {
            this.logger = logger;
        }

        public override bool IsLoadedCorrectly(WebPage page)
        {
            return true;
        }

        public override bool ParseHtml(WebPage page, out RequestInfo requestInfo)
        {
            requestInfo = new RequestInfo();
            return true;
        }

        public override bool CreateAdditionalJobs(WebPage page, out RequestInfo requestInfo)
        {
            requestInfo = new RequestInfo();
            return true;
        }

        public override bool ValidateArguments()
        {
            return true;
        }
    }
}
