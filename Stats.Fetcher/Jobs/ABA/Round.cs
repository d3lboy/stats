using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using Stats.Common.Dto;
using Stats.Common.Enums;
using Stats.Fetcher.Jobs.ABA.Helpers;
using Stats.Fetcher.Library.Clients;
using Stats.Fetcher.Library.Core;
using Competition = Stats.Common.Enums.Competition;

namespace Stats.Fetcher.Jobs.ABA
{
    [JobFlags(Competition.Aba, JobType.Round)]
    public class Round:JobBase
    {
        private readonly ILogger<Round> logger;

        public Round(ILogger<Round> logger, IOptions<AppConfig> appConfig, IApiClient client) : base(logger, client)
        {
            this.logger = logger;
        }

        public override bool IsLoadedCorrectly(WebPage page)
        {
            return page.Html.CssSelect("#accordion").Any();
        }

        public override bool ParseHtml(WebPage page, out RequestInfo requestInfo)
        {
            requestInfo = new RequestInfo();
            
            return true;
        }

        public override bool CreateAdditionalJobs(WebPage page, out RequestInfo requestInfo)
        {
            requestInfo = new RequestInfo();

            var result =  new List<BaseDto>();
            
            page.Html.CssSelect($"#collapse_{Arguments["round"]} tbody>tr").ToList().ForEach(tr =>
                {
                    string url = tr.CssSelect("a").Skip(5).FirstOrDefault()?.Attributes["href"].Value;
                    string date = tr.CssSelect(".locationtable").FirstOrDefault()?.InnerText;
                    
                    result.Add(new JobDto
                    {
                        Args = JsonSerializer.Serialize(Arguments),
                        Competition = Competition.Aba,
                        Parent = JobDto.Id,
                        ScheduledDate = Parser.ToDate(date),
                        Type = JobType.Game,
                        Url = url
                    });
                });

            requestInfo.Data = result;
            return false;
        }

        public override bool ValidateArguments()
        {
            return Arguments.ContainsKey("season") && Arguments.ContainsKey("round");
        }
    }
}
