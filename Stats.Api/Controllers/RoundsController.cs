using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Utilities;
using Stats.Api.Business;
using Stats.Api.Business.Exceptions;
using Stats.Api.Models;
using Stats.Common.Dto;
using Stats.Common.Enums;

namespace Stats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundsController : ControllerBase
    {
        private readonly RoundManager roundManager;
        public RoundsController(StatsDbContext context)
        {
            roundManager = new RoundManager(context);
        }
        // GET: api/Rounds
        //[HttpGet]
        //public async Task<IEnumerable<RoundDto>> Get()
        //{
        //    return roundManager.GetRounds()
        //}

        /// <summary>
        /// Get rounds from a season
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Rounds/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IEnumerable<RoundDto>> Get(Guid id)
        {
            return await roundManager.GetRounds(id);
        }

        // POST: api/Rounds

        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody]List<RoundDto> rounds)
        {
            try
            {
                int result = await roundManager.SaveNewRounds(rounds);
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

                int result = await roundManager.UpdateRound(dto);
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
                int result = await roundManager.DeleteRound(id);
                return Ok(result);
            }
            catch (ItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
