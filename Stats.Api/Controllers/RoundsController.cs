using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stats.Api.Business.Exceptions;
using Stats.Api.Business.Interfaces;
using Stats.Common.Dto;

namespace Stats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundsController : ControllerBase
    {
        private readonly IRoundService service;
        public RoundsController(IRoundService service)
        {
            this.service = service;
        }
        

        /// <summary>
        /// Get rounds from a season
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Rounds/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<RoundDto>> Get(Guid id)
        {
            return await service.GetAsync(id);
        }

        // POST: api/Rounds

        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody]List<RoundDto> rounds)
        {
            try
            {
                await service.AddAsync(rounds);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Rounds/5
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Put(Guid id, [FromBody] RoundDto dto)
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(Guid id)
        {
            try
            {
                int result = await service.DeleteAsync(id);
                return Ok(result);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
