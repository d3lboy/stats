using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using Stats.Common.Dto;
using Stats.Common.Enums;
using Stats.Fetcher.Library.Clients;
using Stats.Fetcher.Library.Core;
using Competition = Stats.Common.Enums.Competition;

namespace Stats.Fetcher.Jobs.ABA
{
    [JobFlags(Competition.Aba, JobType.Rounds)]
    public class Rounds : JobBase
    {
        private readonly ILogger<Rounds> logger;
        private Guid season;

        public Rounds(ILogger<Rounds> logger, IApiClient client) : base(logger, client)
        {
            this.logger = logger;
        }

        public override bool IsLoadedCorrectly(WebPage page)
        {
            return page.Html.CssSelect("#main_content").Any();
        }

        public override bool ParseHtml(WebPage page, out RequestInfo requestInfo)
        {
            requestInfo = new RequestInfo
            {
                Endpoint = "rounds"
            };

            var rounds = new List<BaseDto>();
            page.Html.CssSelect($"#accordion").CssSelect(".panel-default>.panel-heading").ToList().ForEach(x =>
            {
                string txt = x.Id.Replace("heading_", "");

                int num = int.Parse(txt);
                rounds.Add(new RoundDto
                {
                    Season = season,
                    RoundNumber = num,
                    RoundType = RoundType.RegularSeason,
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                });
            });

            logger.LogDebug($"Loaded {rounds.Count} rounds.");
            requestInfo.Data = rounds;
            return true;
        }

        public override bool CreateAdditionalJobs(WebPage page, out RequestInfo requestInfo)
        {
            requestInfo = new RequestInfo();

            var jobs = new List<BaseDto>();

            page.Html.CssSelect($"#accordion").CssSelect(".panel-default>.panel-heading").ToList().ForEach(x =>
            {
                string txt = x.Id.Replace("heading_", "");
                int num = int.Parse(txt);

                Arguments["round"] = num;

                jobs.Add(new JobDto
                {
                    Id = Guid.NewGuid(),
                    State = JobState.New,
                    Competition = Competition.Aba,
                    ScheduledDate = DateTime.Now,
                    CreatedBy = JobDto.Id,
                    Args = JsonConvert.SerializeObject(Arguments),
                    Type = JobType.Round,
                    Url = "https://www.aba-liga.com/calendar.php",
                    Parent = season
                });
            });

            requestInfo.Data = jobs;

            return true;
        }

        public override bool ValidateArguments()
        {
            return Arguments.ContainsKey("season") && Guid.TryParse(Arguments["season"].ToString(), out season);
        }
    }
}
