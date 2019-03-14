using System;
using Microsoft.Extensions.Logging;

namespace Stats.Fetcher.Library.Core
{
    public class CompetitionFactory : ICompetitionFactory
    {
        private readonly ILogger<CompetitionFactory> logger;
        private readonly IServiceProvider serviceProvider;

        public CompetitionFactory(ILogger<CompetitionFactory> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public ICompetition CreateInstance()
        {
            try
            {
                return (ICompetition)serviceProvider.GetService(typeof(ICompetition));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }
    }
}
