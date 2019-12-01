using System;
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
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService service;
        public TeamsController(ITeamService service)
        {
            this.service = service;
        }
        

        /// <summary>
        /// Gets team 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<TeamDto> Get(Guid id)
        {
            return await service.GetAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody]TeamDto dto)
        {
            try
            {
                Log.Verbose($"Add team {dto}");
                await service.AddAsync(dto);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(Guid id, [FromBody] TeamDto dto)
        {
            try
            {
                if(id != dto.Id)
                    return BadRequest();

                int result = await service.UpdateAsync(dto);
                return Ok(result);
            }
            catch(ItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
