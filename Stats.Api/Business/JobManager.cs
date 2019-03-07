using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stats.Api.Business.Exceptions;
using Stats.Api.Models;
using Stats.Common.Dto;
using Stats.Common.Enums;
using Competition = Stats.Common.Enums.Competition;

namespace Stats.Api.Business
{
    public class JobManager
    {
        private readonly StatsDbContext context;

        public JobManager(StatsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<JobDto>> GetJobs()
        {
            var jobs = await context.Jobs.Where(x => x.State == JobState.New)
                .OrderBy(x => x.ScheduledDate).Take(10).ToListAsync();

            return jobs.Any() ? jobs.Select(Map).ToList() : new List<JobDto>();
        }

        public async Task<List<JobDto>> GetJobs(JobFilter filter)
        {
            var result = await context.Jobs.Where(x =>
                        x.State == JobState.New)
                    .OrderBy(x => x.ScheduledDate).Take(100).ToListAsync();

            return result.Any() ? result.Select(Map).ToList() : new List<JobDto>();
        }

        public async Task<JobDto> GetJob(Guid id)
        {
            var job = await context.Jobs.SingleOrDefaultAsync(x => x.Id == id);

            if (job == null)
            {
                throw new ItemNotFoundException();
            }

            return Map(job);
        }

        public async Task<Guid> SaveNewJob(JobDto jobDto)
        {
            var job = await context.Jobs.SingleOrDefaultAsync(x => x.Id == jobDto.Id);
            if (job != null)
            {
                throw new ItemAlreadyExistException($"Job {jobDto.Id} not found!");
            }

            job = Map(jobDto);
            job.Id = Guid.NewGuid();
            context.Jobs.Add(job);

            await context.SaveChangesAsync();

            return job.Id;
        }

        public async Task<int> UpdateJob(JobDto jobDto)
        {
            var job = await context.Jobs.SingleOrDefaultAsync(x => x.Id == jobDto.Id);
            if (job == null)
            {
                throw new ItemNotFoundException();
            }

            job.ScheduledDate = jobDto.ScheduledDate;
            job.Args = jobDto.Args;
            job.ExecutedDate = jobDto.ExecutedDate;
            job.State = jobDto.State;

            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteJob(Guid id)
        {
            var job = await context.Jobs.SingleOrDefaultAsync(x => x.Id == id);
            if (job == null)
            {
                throw new ItemNotFoundException();
            }

            context.Jobs.Remove(job);
            return await context.SaveChangesAsync();
        }

        private JobDto Map(Job job)
        {
            return new JobDto
            {
                State = job.State,
                ScheduledDate = job.ScheduledDate,
                Args = job.Args,
                ExecutedDate = job.ExecutedDate,
                Id = job.Id,
                Url = job.Url
            };
        }

        private Job Map(JobDto job)
        {
            return new Job
            {
                State = job.State,
                ScheduledDate = job.ScheduledDate,
                Args = job.Args,
                ExecutedDate = job.ExecutedDate,
                Id = job.Id,
                Url = job.Url,
                Competition = job.Competition
            };
        }
    }
}
