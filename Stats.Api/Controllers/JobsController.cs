using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stats.Api.Business;
using Stats.Api.Business.Exceptions;
using Stats.Api.Models;
using Stats.Common.Dto;

namespace Stats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly JobManager jobManager;

        public JobsController(StatsDbContext context)
        {
            jobManager = new JobManager(context);
        }

        [HttpGet]
        public async Task<ActionResult<List<JobDto>>> Get()
        {
            var jobs = await jobManager.GetJobs();

            if (!jobs.Any())
            {
                return NotFound();
            }

            return jobs;
        }

        // GET: api/Jobs
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDto>> Get(Guid id)
        {
            try
            {
                var job = await jobManager.GetJob(id);
                return job;
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }

        [Route("fetch")]
        [HttpPost]
        public async Task<ActionResult<List<JobDto>>> Fetch([FromBody] JobFilter filter)
        {
            try
            {
                return await jobManager.GetJobs(filter);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/JobDtoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, JobDto jobDto)
        {
            if (id != jobDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await jobManager.UpdateJob(jobDto);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/JobDtoes
        [HttpPost]
        public async Task<ActionResult<JobDto>> Post(JobDto jobDto)
        {
            try
            {
                jobDto.Id = await jobManager.SaveNewJob(jobDto);
            }
            catch (ItemAlreadyExistException ex)
            {
                return Conflict(ex);
            }

            return Ok(jobDto.Id);
        }

        // DELETE: api/JobDtoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobDto>> Delete(Guid id)
        {
            try
            {
                await jobManager.DeleteJob(id);
            }
            catch (ItemAlreadyExistException ex)
            {
                return Conflict(ex);
            }

            return Ok(id);
        }
    }
}
