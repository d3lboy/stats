using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Stats.Fetcher.Modules
{
    public class JobManager : IHostedService, IDisposable
    {
        private readonly ILogger<JobManager> logger;
        private readonly IOptions<AppConfig> appConfig;

        public JobManager(ILogger<JobManager> logger, IOptions<AppConfig> appConfig, IJob job)
        {
            this.logger = logger;
            this.appConfig = appConfig;
            job.Start();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"Started.");
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
