using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options; 
using Stats.Common.Dto;
using Stats.Fetcher.Library.Browser;
using Stats.Fetcher.Modules;

namespace Stats.Fetcher.Library
{
    public class JobManager : IHostedService, IDisposable
    {
        private ConcurrentDictionary<Guid, JobDto> jobs = new ConcurrentDictionary<Guid, JobDto>();
        private readonly ILogger<JobManager> logger;
        private readonly IOptions<AppConfig> appConfig;
        private readonly Client client;

        public JobManager(ILogger<JobManager> logger, IOptions<AppConfig> appConfig, IJob job, Client client)
        {
            this.logger = logger;
            this.appConfig = appConfig;
            this.client = client;
            //job.Start();
        }

        private async void FetchNewJobs_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string url = appConfig.Value.ApiUrl + "jobs";
                logger.LogInformation($"Get new jobs from {url}");
                var newJobs = await client.GetObject<List<JobDto>>(url);

                logger.LogDebug($"Loaded {newJobs.Count} jobs.");
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"Started.");
            var fetchNewJobs = new System.Timers.Timer()
            {
                AutoReset = false,
                Interval = 1000
            };

            fetchNewJobs.Elapsed += FetchNewJobs_Elapsed;
            fetchNewJobs.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"Stopped.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //
        }
    }
}
