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
    [JobFlags(Common.Enums.Competition.Aba, JobType.Round)]
    public class Round:JobBase
    {
        private readonly ILogger<JobBase> logger;

        public Round(ILogger<JobBase> logger, IOptions<AppConfig> appConfig, IApiClient client) : base(logger, appConfig, client)
        {
            this.logger = logger;
        }

        public override bool IsLoadedCorrectly(WebPage page)
        {
            return true;
        }

        public override bool ParseHtml(WebPage page, out List<BaseDto> result)
        {
            result = new List<BaseDto>();
            logger.LogDebug($"Parsed.");
            return true;
        }

        public override bool CreateAdditionalJobs(WebPage page, out List<BaseDto> dtos)
        {
            dtos = new List<BaseDto>();
            return false;
        }

        public override void ParseArguments(string args)
        {
            //
        }
    }
}
