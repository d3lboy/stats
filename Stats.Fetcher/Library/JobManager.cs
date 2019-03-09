using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stats.Common.Dto;
using Stats.Common.Enums;
using Stats.Fetcher.Jobs.ABA;
using Stats.Fetcher.Library.Clients;
using Stats.Fetcher.Library.Core;

namespace Stats.Fetcher.Library
{
    public class JobManager : IHostedService, IDisposable
    {
        private readonly ConcurrentDictionary<Guid, JobDto> jobs = new ConcurrentDictionary<Guid, JobDto>();
        private readonly ILogger<JobManager> logger;
        private readonly IOptions<AppConfig> appConfig;
        private readonly IServiceProvider serviceProvider;
        private readonly IApiClient client;

        public JobManager(ILogger<JobManager> logger, IOptions<AppConfig> appConfig, IServiceProvider serviceProvider, IApiClient client)
        {
            this.logger = logger;
            this.appConfig = appConfig;
            this.serviceProvider = serviceProvider;
            this.client = client;
        }

        private async Task StartNewJob()
        {
            //new Job(serviceProvider.GetService<ILogger<Job>>());
            //serviceProvider.GetService<I>()
            await new Rounds(logger, appConfig, client).ProcessJob("http://google.com");
        }

        public List<KeyValuePair<Competition, int>> JobsPerCompetition =>
            jobs.Values.ToList()
                .GroupBy(g => g.Competition)
                .Select(x => new KeyValuePair<Competition, int>(x.Key, x.Count())).ToList();

        private async void FetchNewJobs_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (jobs.Count > 50) return;//TODO parameterize
            await GetNewJobs();
        }

        private async Task GetNewJobs()
        {
            try
            {
                var newJobs = await client.Post<List<JobDto>>("jobs/fetch", new JobFilter());

                UpdateCache(newJobs);
                logger.LogDebug($"Loaded {newJobs.Count} jobs. By competition: {this}");
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
            }
        }
        private void UpdateCache(List<JobDto> newJobs)
        {
            newJobs.ForEach(job =>
            {
                if (!jobs.ContainsKey(job.Id)) jobs.TryAdd(job.Id, job);
            });
        }

        public async  Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"Started.");
            var fetchNewJobs = new System.Timers.Timer()
            {
                AutoReset = true,
                Interval = 10000
            };

            await StartNewJob();

            fetchNewJobs.Elapsed += FetchNewJobs_Elapsed;
            fetchNewJobs.Start();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"Stopped.");
            return Task.CompletedTask;
        }

        public override string ToString()
        {
            return $"{string.Join(",", JobsPerCompetition.Select(x => $"{x.Key.ToString()}({x.Value})"))}";
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
