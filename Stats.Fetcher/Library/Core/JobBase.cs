using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public abstract class JobBase : IJobBase
    {
        private readonly ILogger<JobBase> logger;
        private readonly IApiClient client;
        protected JobDto JobDto;

        protected JobBase(ILogger<JobBase> logger, IOptions<AppConfig> appConfig, IApiClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task<bool> ProcessJob(JobDto dto)
        {
            JobDto = dto;

            var page = await new Browser().GetPage(dto.Url);

            if (!IsLoadedCorrectly(page)) return false;

            ParseArguments(dto.Args);

            if (!ParseHtml(page, out var items) || !items.Any())
            {
                return false;
            }

            var item = items.First();
            if(!await client.Post<bool>(item.Source, items))
            {
                return false;
            }

            CreateAdditionalJobs(page, out var jobs);

            if (!jobs.Any()) return true;

            return await client.Post<bool>(jobs.First().Source, jobs);
        }

        public abstract bool IsLoadedCorrectly(WebPage page);
        public abstract bool ParseHtml(WebPage page, out List<BaseDto> result);
        public abstract bool CreateAdditionalJobs(WebPage page, out List<BaseDto> dtos);
        public abstract void ParseArguments(string args);

        public virtual int? RescheduleInterval { get; set; }

        private void ReleaseUnmanagedResources()
        {
            //
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                client?.Dispose();
            }
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
