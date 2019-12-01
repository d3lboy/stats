using Microsoft.Extensions.DependencyInjection;
using Stats.Api.Business;
using Stats.Api.Business.Interfaces;

namespace Stats.Api
{
    public static class DiRegistry
    {
        public static void Init(IServiceCollection services)
        {
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IRoundService, RoundService>();
            services.AddTransient<IReferenceService, ReferenceService>();
            services.AddTransient<ITeamService, TeamService>();
        }
    }
}
