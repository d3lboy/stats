using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using Stats.Common.Dto;
using Stats.Common.Enums;
using Stats.Fetcher.Library.Clients;
using Stats.Fetcher.Library.Core;
using Stats.Fetcher.Library.Extensions;
using Competition = Stats.Common.Enums.Competition;

namespace Stats.Fetcher.Jobs.ABA
{
    [JobFlags(Competition.Aba, JobType.Player)]
    public class Player : JobBase
    {
        private readonly ILogger<Player> logger;

        public Player(ILogger<Player> logger, IApiClient client) : base(logger, client)
        {
            this.logger = logger;
        }

        public override bool IsLoadedCorrectly(WebPage page)
        {
            return page.Html.CssSelect(".information-block").Any();
        }

        public override bool ParseHtml(WebPage page, out RequestInfo requestInfo)
        {
            requestInfo = new RequestInfo
            {
                Endpoint = "teams",
                SingleDto = true
            };
            string address = page.Html.CssSelect(".information-block .overlay>p>span").FirstOrDefault()?.InnerText?.TrimFull();
            string name = page.Html.CssSelect(".information-block>.club-information>span").FirstOrDefault()?.InnerText?.TrimFull();
            string url = page.Html.CssSelect(".information-block>.club-info>.overlay a").Skip(1).FirstOrDefault()?.InnerText?.TrimFull();
            TeamDto team = new TeamDto
            {
                ReferenceId = Arguments["id"].ToString(),
                Country = ParseCountry(address),
                Address = address,
                Name = name,
                Url = url,
                Competition = Competition.Aba
            };

            requestInfo.Data = new List<BaseDto> { team };

            logger.LogInformation($"Parsed team {team}");
            return true;
        }

        public override bool CreateAdditionalJobs(WebPage page, out RequestInfo requestInfo)
        {
            requestInfo = new RequestInfo();
            var result = new List<BaseDto>();

            page.Html.CssSelect(".teamroster>table>tbody>tr").ToList().ForEach(tr =>
                {
                    string url = tr.CssSelect("a").FirstOrDefault()?.Attributes["href"].Value;

                    Arguments.Clear(); 

                    result.Add(new JobDto
                    {
                        Args = JsonSerializer.Serialize(Arguments),
                        Competition = Competition.Aba,
                        Parent = JobDto.Id,
                        ScheduledDate = DateTime.Now,
                        Type = JobType.Player,
                        Url = url
                    });
                });

            requestInfo.Data = result;
            return false;
        }

        private Country ParseCountry(string text)
        {
            text = text.ToLowerInvariant();
            if (text.Contains("serbia"))
                return Country.Serbia;
            else if (text.Contains("montenegro"))
                return Country.Montenegro;
            else if (text.Contains("macedonia"))
                return Country.Macedonia;
            else if (text.Contains("slovenia"))
                return Country.Slovenia;
            else if (text.Contains("bosnia"))
                return Country.Bosnia;
            else if (text.Contains("croatia"))
                return Country.Croatia;

            return Country.Unknown;
        }

        public override bool ValidateArguments()
        {
            return Arguments.ContainsKey("id");
        }
    }
}
