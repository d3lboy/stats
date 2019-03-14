using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stats.Common.Dto;
using Stats.Fetcher.Library.Clients;

namespace Stats.Fetcher.Library.Core
{
    public class Synchronizer : ISynchronizer
    {
        private readonly ILogger<Synchronizer> logger;
        private readonly IOptions<AppConfig> appConfig;
        private readonly ICache cache;
        private readonly IApiClient client;
        private readonly Timer checkTimer;

        public Synchronizer(ILogger<Synchronizer> logger, IOptions<AppConfig> appConfig, ICache cache, IApiClient client)
        {
            this.logger = logger;
            this.appConfig = appConfig;
            this.cache = cache;
            this.client = client;

            this.cache.JobFinished+=JobFinished;
            this.cache.JobUpdated+=JobUpdated;

            Task.Run(async () => { await FetchNewJobs();});

            checkTimer = new Timer(appConfig.Value.CheckFrequency * 1000) {AutoReset = true};
            checkTimer.Elapsed += CheckTimerElapsed;
            checkTimer.Start();
        }

        public async Task FetchNewJobs()
        {
            await DownloadNewJobs();
        }

        private void JobUpdated(JobDto job)
        {
            Task.Run(async () => { await UpdateJob(job);});
        }

        private async void CheckTimerElapsed(object sender, ElapsedEventArgs e)
        {
            await DownloadNewJobs();
        }

        private void JobFinished(JobDto obj)
        {
            Task.Run(async ()=>
            {
                await DownloadNewJobs();
            });
        }

        private async Task DownloadNewJobs()
        {
            checkTimer.Stop();
            
            try
            {
                if (cache.Count <= appConfig.Value.MinJobsInCache)
                {
                    var newJobs = await client.Post<List<JobDto>>("jobs/fetch", new JobFilter());
                    cache.Add(newJobs);
                    logger.LogInformation($"Retreived {newJobs.Count} job(s) from the server.");
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
            }

            checkTimer.Start();
        }

        private async Task<bool> UpdateJob(JobDto job)
        {
            job.Source = $"jobs/{job.Id.ToString()}";
            return await client.Put(job);
        }
    }
}
