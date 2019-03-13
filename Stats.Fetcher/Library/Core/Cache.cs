using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stats.Common.Dto;
using Stats.Common.Enums;

namespace Stats.Fetcher.Library.Core
{
    public class Cache : ICache
    {
        private readonly ILogger<JobManager> logger;
        private readonly IOptions<AppConfig> appConfig;
        private readonly ConcurrentDictionary<Guid, JobDto> jobs = new ConcurrentDictionary<Guid, JobDto>();

        public Cache(ILogger<JobManager> logger, IOptions<AppConfig> appConfig)
        {
            this.logger = logger;
            this.appConfig = appConfig;
        }

        public Action<JobDto> Updated { get; set; }

        public List<KeyValuePair<Competition, int>> JobsPerCompetition =>
            jobs.Values.ToList()
                .GroupBy(g => g.Competition)
                .Select(x => new KeyValuePair<Competition, int>(x.Key, x.Count())).ToList();

        public JobDto GetJobCandidate(Competition competition)
        {
            var competitionJobs = jobs.Values.Where(x => x.Competition == competition && x.State != JobState.Finished)
                .ToList();
            return competitionJobs.Any(x => x.State == JobState.InProgress) ?
                null :
                competitionJobs.OrderByDescending(x => x.ScheduledDate).FirstOrDefault();
        }

        public void Add(List<JobDto> newJobs)
        {
            newJobs.ForEach(job =>
            {
                if (!jobs.ContainsKey(job.Id)) jobs.TryAdd(job.Id, job);
            });
        }

        public void Update(JobDto dto)
        {
            try
            {
                if (jobs.TryGetValue(dto.Id, out var job))
                {
                    job.State = dto.State;
                    job.ExecutedDate = DateTime.Now;
                    Updated?.Invoke(job);
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
    }
}
