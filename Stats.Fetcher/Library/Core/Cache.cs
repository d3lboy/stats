using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var competitionJobs = jobs.Values.Where(x => x.Competition == competition && x.State != JobState.Finished)
                .ToList();
            return competitionJobs.Any(x => x.State == JobState.InProgress) ?
                null :
                competitionJobs.OrderByDescending(x => x.ScheduledDate).FirstOrDefault();
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

            if(addedJobs.Any())
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

                    if (job.State == JobState.Finished || job.State == JobState.Error)
                        JobFinished?.Invoke(job);
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
