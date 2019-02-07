using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace StatsFetcher.modules
{
    public class Job : IJob
    {
        private readonly ILogger<Job> logger;

        public Job(ILogger<Job> logger)
        {
            this.logger = logger;
        }

        async Task<bool> IJob.Start()
        {
            await Task.Run((() =>
            {
                logger.LogDebug($"Job started.");
            }));
            return true;
        }
    }
}
