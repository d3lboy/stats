using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stats.Api.Business.Exceptions;
using Stats.Api.Models;
using Stats.Common.Dto;
using Stats.Common.Enums;

namespace Stats.Api.Business
{
    public class JobManager : IJobManager
    {
        private readonly StatsDbContext context;
        private readonly IMapper mapper;

        public JobManager(StatsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<JobDto>> GetJobs()
        {
            var jobs = await context.Jobs.Where(x => x.State == JobState.New)
                .OrderBy(x => x.ScheduledDate).Take(10).ToListAsync();

            return jobs.Any() ? jobs.Select(mapper.Map<JobDto>).ToList() : new List<JobDto>();
        }

        public async Task<List<JobDto>> GetJobs(JobFilter filter)
        {
            var result = await context.Jobs.Where(x =>
                        x.State == JobState.New)
                    .OrderBy(x => x.ScheduledDate).Take(100).ToListAsync();

            return result.Any() ? result.Select(mapper.Map<JobDto>).ToList() : new List<JobDto>();
        }

        public async Task<JobDto> GetJob(Guid id)
        {
            var job = await context.Jobs.SingleOrDefaultAsync(x => x.Id == id);

            if (job == null)
            {
                throw new ItemNotFoundException();
            }

            return mapper.Map<JobDto>(job);
        }

        public async Task<Guid> SaveNewJob(JobDto jobDto)
        {
            var job = await context.Jobs.SingleOrDefaultAsync(x => x.Id == jobDto.Id);
            if (job != null)
            {
                throw new ItemAlreadyExistException($"Job {jobDto.Id} not found!");
            }

            job = mapper.Map<Job>(jobDto);
            job.Id = Guid.NewGuid();
            context.Jobs.Add(job);

            await context.SaveChangesAsync();

            return job.Id;
        }

        public async Task<bool> BulkInsert(List<JobDto> jobs)
        {
            var creator = jobs.First().Id;          
            var existingJobs = await context.Jobs.Where(x => x.CreatedBy == creator).Select(x=>x.Id).ToListAsync();

            jobs.ForEach(dto =>
            {
                if (existingJobs.Contains(dto.Id)) return;

                var job = mapper.Map<Job>(dto);
                job.Id = Guid.NewGuid();
                context.Jobs.Add(job);
            });

            await context.SaveChangesAsync();

            return true;
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
    }
}
