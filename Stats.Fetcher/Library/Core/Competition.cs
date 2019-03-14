using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Stats.Common.Enums;

namespace Stats.Fetcher.Library.Core
{
    public class Competition : ICompetition
    {
        private readonly ILogger<Competition> logger;
        private readonly IJobFactory jobFactory;
        private readonly ICache cache;
        private IJobBase currentJob;
        private Common.Enums.Competition competition;

        public Competition(ILogger<Competition> logger, IJobFactory jobFactory, ICache cache)
        {
            this.logger = logger;
            this.jobFactory = jobFactory;
            this.cache = cache;
        }

        public void Initialize(Common.Enums.Competition competitionIdentifier)
        {
            competition = competitionIdentifier;
            cache.JobFinished += async dto => { await RunNextJob(); };
            cache.JobsAdded += async dtos => { await RunNextJob(); };
        }

        private async Task RunNextJob()
        {
            if (currentJob != null) return;

            var job = cache.GetJobCandidate(competition);

            if(job==null) return;

            logger.LogDebug($"New job selected. {job}");
            job.State = JobState.InProgress;
            cache.Update(job);
            currentJob = jobFactory.CreateInstance(competition, job.Type);

            if (currentJob == null)
            {
                logger.LogError($"Job instance has not been created! {job}");
                return;
            }

            bool result = await currentJob.ProcessJob(job);
            
            job.State = result ? JobState.Finished : JobState.New;

            cache.Update(job);
            currentJob.Dispose();
            currentJob = null;
        }
    }
}
