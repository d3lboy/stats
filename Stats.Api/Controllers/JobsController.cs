using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stats.Api.Business;
using Stats.Api.Business.Exceptions;
using Stats.Common.Dto;

namespace Stats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobs jobRepository;

        public JobsController(IJobs jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<JobDto>>> Get()
        {
            var jobs = await jobRepository.Get();

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
                var job = await jobRepository.Get(id);
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
                return await jobRepository.Get(filter);
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
                await jobRepository.Update(jobDto);
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
                jobDto.Id = await jobRepository.Add(jobDto);
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
                bool result = await jobRepository.Add(jobs);
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
                await jobRepository.Delete(id);
            }
            catch (ItemAlreadyExistException ex)
            {
                return Conflict(ex);
            }

            return Ok(id);
        }
    }
}
