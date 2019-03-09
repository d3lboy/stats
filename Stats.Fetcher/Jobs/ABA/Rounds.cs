using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScrapySharp.Network;
using Stats.Common.Dto;
using Stats.Fetcher.Library.Clients;
using Stats.Fetcher.Library.Core;

namespace Stats.Fetcher.Jobs.ABA
{
    public class Rounds:Job
    {
        public Rounds(ILogger logger, IOptions<AppConfig> appConfig, IApiClient client) : base(logger, appConfig, client)
        {

        }

        public override bool IsLoadedCorrectly(WebPage page)
        {
            return true;
        }

        public override bool ParseHtml(WebPage page, out List<BaseDto> result)
        {
            result = new List<BaseDto> {new RoundDto(){Id = 111}};

            return true;
        }

        public override bool CreateAdditionalJobs(WebPage page, out List<BaseDto> dtos)
        {
            dtos = new List<BaseDto>(){new JobDto(){Id = Guid.NewGuid()}};

            return true;
        }
    }
}
