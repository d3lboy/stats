using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ScrapySharp.Network;
using Stats.Common.Dto;
using Stats.Fetcher.Library.Clients;

namespace Stats.Fetcher.Library.Core
{
    /// <summary>
    /// TODO: Document
    /// </summary>
    public abstract class JobBase : IJobBase
    {
        private readonly ILogger<JobBase> logger;
        private readonly IApiClient client;
        protected JobDto JobDto;
        public Dictionary<string, object> Arguments { get; set; }
    

        protected JobBase(ILogger<JobBase> logger, IApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<bool> ProcessJob(JobDto dto)
        {
            JobDto = dto;

            var page = await new Browser().GetPage(dto.Url);

            if (!IsLoadedCorrectly(page))
            {
                logger.LogWarning("Content has not been loaded correctly.");
                return false;
            }

            if (!ParseArguments(dto.Args))
            {
                logger.LogWarning("Can't parse arguments.");
                return false;
            }

            if (!ValidateArguments())
            {
                logger.LogWarning("Arguments are not valid.");
                return false;
            }

            if (!ParseHtml(page, out var postInfo) || postInfo == null || !postInfo.Data.Any())
            {
                logger.LogWarning("Can't find items.");
            }
            else
            {
                bool result = (postInfo.SingleDto) ?
                    !await client.Post<bool>(postInfo.Endpoint, postInfo.Data.First()):
                    await client.Post<bool>(postInfo.Endpoint, postInfo.Data);
                if (!result)
                {
                    logger.LogWarning($"Bad response from a server.");
                    return false;
                }
            }
          
            CreateAdditionalJobs(page, out var jobs);

            if (!jobs.Data.Any()) return true;

            return await client.Post<bool>("jobs/bulkinsert", jobs.Data);
        }

        public abstract bool IsLoadedCorrectly(WebPage page);
        public abstract bool ParseHtml(WebPage page, out RequestInfo requestInfo);
        public abstract bool CreateAdditionalJobs(WebPage page, out RequestInfo requestInfo);
        public abstract bool ValidateArguments();

        public virtual int? RescheduleInterval { get; set; }

        private bool ParseArguments(string args)
        {
            try
            {
                if (args.Trim() == "")
                {
                    Arguments = new Dictionary<string, object>();
                    return true;
                }

                Arguments = JsonConvert.DeserializeObject<Dictionary<string, object>>(args);

                return true;
            }
            catch (Exception)
            {
                Arguments = new Dictionary<string, object>();
                logger.LogWarning($"Arguments are not valid. {this}");
                return false;
            }
        }

        private void ReleaseUnmanagedResources()
        {
            Arguments = null;
            JobDto = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~JobBase()
        {
            Dispose(false);
        }
    }
}
