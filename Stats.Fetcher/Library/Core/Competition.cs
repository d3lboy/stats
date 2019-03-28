using System;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Logging;
using Stats.Common.Enums;

namespace Stats.Fetcher.Library.Core
{
    public class Competition : ICompetition
    {
        private readonly ILogger<Competition> logger;
        private readonly IJobFactory jobFactory;
        private readonly ICache cache;
        private Common.Enums.Competition competition;
        private readonly Timer checkAgain;

        public Competition(ILogger<Competition> logger, IJobFactory jobFactory, ICache cache)
        {
            this.logger = logger;
            this.jobFactory = jobFactory;
            this.cache = cache;

            checkAgain = new Timer(60000){AutoReset = false};
            checkAgain.Elapsed += async (sender, args) => await RunNextJob("timer elapsed");
        }

        public void Initialize(Common.Enums.Competition competitionIdentifier)
        {
            competition = competitionIdentifier;
            cache.JobFinished += async dto => { await RunNextJob("job finished"); };
            cache.JobsAdded += async dtos => { await RunNextJob("jobs added"); };
        }

        private async Task RunNextJob(string reason)
        {
            logger.LogDebug($"Run new job, reason: {reason}");

            var job = cache.GetJobCandidate(competition);

            if (job == null)
            {
                logger.LogDebug($"Competition: {competition} no job selected.");
                checkAgain.Start();
                return;
            }

            logger.LogDebug($"New job selected. {job}");
            job.State = JobState.InProgress;
            cache.Update(job);
            var currentJob = jobFactory.CreateInstance(competition, job.Type);

            if (currentJob == null)
            {
                logger.LogError($"Job instance has not been created! {job}");
                return;
            }

            bool result = await currentJob.ProcessJob(job);

            if (result)
            {
                cache.Finish(job);
            }
            else if(currentJob.RescheduleInterval.HasValue && currentJob.RescheduleInterval.Value != 0)
            {
                cache.Reschedule(job.Id, TimeSpan.FromSeconds(currentJob.RescheduleInterval.Value));
            }
            else
            {
                cache.Cancel(job);
            }
            
            currentJob.Dispose();
        }
    }
}
