using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stats.Common.Dto;
using Stats.Common.Enums;
using Stats.Fetcher.Library.Clients;

namespace Stats.Fetcher.Library.Core
{
    public class Cache : ICache
    {
        private readonly ILogger<JobManager> logger;
        private readonly IOptions<AppConfig> appConfig;
        private readonly IApiClient client;
        private readonly ConcurrentDictionary<Guid, JobDto> jobs = new ConcurrentDictionary<Guid, JobDto>();

        public Cache(ILogger<JobManager> logger, IOptions<AppConfig> appConfig, IApiClient client)
        {
            this.logger = logger;
            this.appConfig = appConfig;
            this.client = client;
        }

        public Action<List<JobDto>> JobsAdded { get; set; }
        public Action<JobDto> JobUpdated { get; set; }
        public Action<JobDto> JobFinished { get; set; }

        public List<KeyValuePair<Common.Enums.Competition, int>> JobsPerCompetition =>
            jobs.Values.ToList()
                .GroupBy(g => g.Competition)
                .Select(x => new KeyValuePair<Common.Enums.Competition, int>(x.Key, x.Count())).ToList();

        public JobDto GetJobCandidate(Common.Enums.Competition competition)
        {
            var competitionJobs = jobs.Values.Where(x => 
                                                        x.Competition == competition 
                                                        && x.ScheduledDate < DateTime.Now 
                                                        && x.State!=JobState.Error && x.State != JobState.Finished)
                .ToList();
            return competitionJobs.Any(x => x.State == JobState.InProgress) ?
                null :
                competitionJobs.OrderBy(x => x.ScheduledDate).FirstOrDefault();
        }

        public int Count => jobs.Count;

        public void Add(List<JobDto> newJobs)
        {
            var addedJobs = new List<JobDto>();
            newJobs.ForEach(job =>
            {
                if (jobs.ContainsKey(job.Id)) return;

                jobs.TryAdd(job.Id, job);
                addedJobs.Add(job);
            });

            if (addedJobs.Any())
                JobsAdded?.Invoke(addedJobs);
        }

        public void Update(JobDto dto)
        {
            try
            {
                if (jobs.TryGetValue(dto.Id, out var job))
                {
                    job.State = dto.State;
                    job.ExecutedDate = DateTime.Now;
                    JobUpdated?.Invoke(job);

                    logger.LogDebug($"{job}");

                    if (job.State != JobState.Finished && job.State != JobState.Error) return;

                    JobFinished?.Invoke(job);
                    Clean();
                }
                else
                {
                    logger.LogWarning($"Can't find job with Id: {dto.Id}");
                }
            }
            catch (Exception exception)
            {
                logger.LogError($"Update error: {dto}, {exception.Message}");
            }
        }

        public void Finish(JobDto dto)
        {
            dto.State = JobState.Finished;
            Update(dto);
        }

        public void Cancel(JobDto dto)
        {
            dto.State = JobState.Error;
            Update(dto);
        }

        public void Reschedule(Guid id, TimeSpan interval)
        {
            if (!jobs.TryGetValue(id, out var job)) return;

            job.State = JobState.New;
            job.ScheduledDate = DateTime.Now.Add(interval);

            JobUpdated?.Invoke(job);
        }

        public void Reschedule(Guid id)
        {
            Reschedule(id, TimeSpan.FromSeconds(appConfig.Value.DefaultRescheduleDelay));
        }

        private void Clean()
        {
            List<JobDto> removedJobs = new List<JobDto>();
            jobs.Values.Where(x => x.State == JobState.Error || x.State == JobState.Finished)
                .Select(x => x.Id).ToList().ForEach(id =>
                {
                    if (jobs.TryRemove(id, out var job))
                    {
                        removedJobs.Add(job);
                    }
                });

            if (removedJobs.Any())
                logger.LogDebug($"Cleaned {removedJobs.Count} job(s)");
        }
    }
}
