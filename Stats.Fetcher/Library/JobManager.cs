using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stats.Common.Dto;
using Stats.Common.Enums;
using Stats.Fetcher.Library.Clients;
using Stats.Fetcher.Library.Core;

namespace Stats.Fetcher.Library
{
    public class JobManager : IHostedService, IDisposable
    {
        
        private readonly ILogger<JobManager> logger;
        private readonly IOptions<AppConfig> appConfig;
        private readonly IServiceProvider serviceProvider;
        private readonly IJobFactory jobFactory;
        private readonly IApiClient client;
        private readonly ICache cache;

        public JobManager(ILogger<JobManager> logger, IOptions<AppConfig> appConfig, IServiceProvider serviceProvider, IJobFactory jobFactory, IApiClient client, ICache cache)
        {
            this.logger = logger;
            this.appConfig = appConfig;
            this.serviceProvider = serviceProvider;
            this.jobFactory = jobFactory;
            this.client = client;
            this.cache = cache;
        }

        private async Task<bool> UpdateJob(JobDto job)
        {
            job.Source = $"jobs/{job.Id.ToString()}";
            return await client.Put(job);
        }
        private async Task RunJobs()
        {
            foreach (var value in Enum.GetValues(typeof(Competition)).Cast<Competition>())
            {
                var job = cache.GetJobCandidate(value);
                if (job != null)
                    await StartNewJob(job);
            }
        }
        
        private async Task StartNewJob(JobDto job)
        {
            logger.LogDebug($"New job ready. {job}");
            job.State = JobState.InProgress;
            cache.Update(job);
            var jobInstance = jobFactory.CreateInstance(Competition.Aba, JobType.Rounds);
            bool result = await jobInstance.ProcessJob(job);
            logger.LogDebug($"Job execution finished. Result:{result}");
            job.State = result ? JobState.Finished : JobState.New;
            cache.Update(job);
            jobInstance.Dispose();
        }

        

        private async void FetchNewJobs_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //TODO parameterize
            await GetNewJobs();
        }

        private async Task GetNewJobs()
        {
            try
            {
                var newJobs = await client.Post<List<JobDto>>("jobs/fetch", new JobFilter());

                cache.Add(newJobs);
                if(newJobs.Any())
                    logger.LogDebug($"Loaded {newJobs.Count} jobs. By competition: {this}");
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
                Interval = 2000
            };

            fetchNewJobs.Elapsed += FetchNewJobs_Elapsed;
            fetchNewJobs.Start();

            var dummy = new System.Timers.Timer(5000) {AutoReset = true};
            dummy.Elapsed += Dummy_Elapsed;
            dummy.Start();

            return Task.CompletedTask;
        }

        private void Dummy_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Task.Run(RunJobs);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"Stopped.");
            return Task.CompletedTask;
        }

        public override string ToString()
        {
            return $"{string.Join(",", cache.JobsPerCompetition.Select(x => $"{x.Key.ToString()}({x.Value})"))}";
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
