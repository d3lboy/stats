using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stats.Common.Enums;
using Stats.Fetcher.Jobs.ABA;
using Stats.Fetcher.Library.Clients;

namespace Stats.Fetcher.Library.Core
{
    public class JobFactory : IJobFactory
    {
        private readonly ILogger<JobFactory> logger;
        private readonly IOptions<AppConfig> appConfig;
        private readonly IServiceProvider serviceProvider;
        private readonly IApiClient client;

        public JobFactory(ILogger<JobFactory> logger, IOptions<AppConfig> appConfig, IServiceProvider serviceProvider, IApiClient client)
        {
            this.logger = logger;
            this.appConfig = appConfig;
            this.serviceProvider = serviceProvider;
            this.client = client;

            LoadAllJobs();
        }

        public IJobBase CreateInstance(Competition competition, JobType type)
        {
            var job = Jobs.SingleOrDefault(x => x.Competition == competition && x.Type == type);
            if (job == null) return null;
            try
            {
                return (IJobBase)serviceProvider.GetService(job.ObjectType);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public List<JobInfo> Jobs { get; protected set; }

        private void LoadAllJobs()
        {
            Jobs = new List<JobInfo>();
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IJobBase).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList()
                .ForEach(x =>
                {
                    JobFlags flag = (JobFlags)Attribute.GetCustomAttribute(x, typeof(JobFlags));

                    if (flag != null)
                    {
                        Jobs.Add(new JobInfo
                        {
                            Competition = flag.Competition,
                            Type = flag.Type,
                            ObjectType = x
                        });
                    }
                });
        }
    }
}
