using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using Stats.Common.Dto;
using Stats.Common.Enums;
using Stats.Fetcher.Library.Clients;
using Stats.Fetcher.Library.Core;

namespace Stats.Fetcher.Jobs.ABA
{
    [JobFlags(Competition.Aba, JobType.Rounds)]
    public class Rounds : JobBase
    {
        private readonly ILogger<Rounds> logger;
        private Guid seasonId;

        public Rounds(ILogger<Rounds> logger, IOptions<AppConfig> appConfig, IApiClient client) : base(logger, appConfig, client)
        {
            this.logger = logger;
        }

        public override bool IsLoadedCorrectly(WebPage page)
        {
            bool contentExist = page.Html.CssSelect("#main_content").Any();
            logger.LogTrace($"Content exists {contentExist}");
            return contentExist;
        }

        public override bool ParseHtml(WebPage page, out List<BaseDto> result)
        {
            var rounds = new List<BaseDto>();
            page.Html.CssSelect($"#accordion").CssSelect(".panel-default>.panel-heading").ToList().ForEach(x =>
            {
                string txt = x.Id.Replace("heading_", "");

                int num = int.Parse(txt);
                rounds.Add(new RoundDto
                {
                    Source = "rounds",
                    Season = seasonId,
                    RoundNumber = num,
                    RoundType = RoundType.RegularSeason,
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now
                });
            });

            logger.LogDebug($"Loaded {rounds.Count} rounds.");
            result = rounds;
            return true;
        }

        public override bool CreateAdditionalJobs(WebPage page, out List<BaseDto> dtos)
        {
            dtos = new List<BaseDto>() {  };

            return true;
        }

        public override void ParseArguments(string args)
        {
            seasonId = Guid.Parse(args);
        }
    }
}
