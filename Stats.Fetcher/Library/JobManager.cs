using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Stats.Fetcher.Library.Core;
using Competition = Stats.Common.Enums.Competition;

namespace Stats.Fetcher.Library
{
    public class JobManager : IHostedService, IDisposable
    {
        
        private readonly ILogger<JobManager> logger;
        private readonly ISynchronizer synchronizer;
        private readonly ICompetitionFactory competitionFactory;

        public JobManager(ILogger<JobManager> logger, ISynchronizer  synchronizer, ICompetitionFactory competitionFactory)
        {
            this.logger = logger;
            this.synchronizer = synchronizer;
            this.competitionFactory = competitionFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"Started.");

            foreach (int value in Enum.GetValues(typeof(Competition)))
            {
                competitionFactory.CreateInstance().Initialize((Competition)value);
            }

            synchronizer.FetchNewJobs();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"Stopped.");
            return Task.CompletedTask;
        }

        
      
        public void Dispose()
        {
            
        }
    }
}
