using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Stats.Api.Business.Exceptions;
using Stats.Api.Business.Interfaces;
using Stats.Common.Dto;

namespace Stats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService service;

        public JobsController(IJobService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<JobDto>>> Get()
        {
            var jobs = await service.GetAsync();
            Log.Debug($"Loaded {jobs.Count} jobs.");
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
                var job = await service.GetAsync(id);
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
                return await service.GetAsync(filter);
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
                await service.UpdateAsync(jobDto);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/JobDtoes
        [HttpPost]
        public async Task<ActionResult<JobDto>> Post([FromBody] JobDto jobDto)
        {
            try
            {
                jobDto.Id = await service.AddAsync(jobDto);
            }
            catch (ItemAlreadyExistException ex)
            {
                return Conflict(ex);
            }

            return Ok(jobDto.Id);
        }

        [HttpPost]
        [Route("bulkinsert")]
        public async Task<ActionResult<bool>> BulkInsert([FromBody] List<JobDto> jobs)
        {
            try
            {
                bool result = await service.AddAsync(jobs);
                return Ok(result);
            }
            catch (ItemAlreadyExistException ex)
            {
                return Conflict(ex);
            }
        }

        // DELETE: api/JobDtoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobDto>> Delete(Guid id)
        {
            try
            {
                await service.DeleteAsync(id);
            }
            catch (ItemAlreadyExistException ex)
            {
                return Conflict(ex);
            }

            return Ok(id);
        }
    }
}
