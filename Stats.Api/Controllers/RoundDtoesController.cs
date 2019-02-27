using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stats.Api.Models;
using Stats.Common;
using Stats.Common.Dto;

namespace Stats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundDtoesController : ControllerBase
    {
        private readonly StatsDbContext _context;

        public RoundDtoesController(StatsDbContext context)
        {
            _context = context;
        }

        // GET: api/RoundDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoundDto>>> GetRoundDto()
        {
            return await _context.RoundDto.ToListAsync();
        }

        // GET: api/RoundDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoundDto>> GetRoundDto(int id)
        {
            var roundDto = await _context.RoundDto.FindAsync(id);

            if (roundDto == null)
            {
                return NotFound();
            }

            return roundDto;
        }

        // PUT: api/RoundDtoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoundDto(int id, RoundDto roundDto)
        {
            if (id != roundDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(roundDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoundDtoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RoundDtoes
        [HttpPost]
        public async Task<ActionResult<RoundDto>> PostRoundDto(RoundDto roundDto)
        {
            _context.RoundDto.Add(roundDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoundDto", new { id = roundDto.Id }, roundDto);
        }

        // DELETE: api/RoundDtoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RoundDto>> DeleteRoundDto(int id)
        {
            var roundDto = await _context.RoundDto.FindAsync(id);
            if (roundDto == null)
            {
                return NotFound();
            }

            _context.RoundDto.Remove(roundDto);
            await _context.SaveChangesAsync();

            return roundDto;
        }

        private bool RoundDtoExists(int id)
        {
            return _context.RoundDto.Any(e => e.Id == id);
        }
    }
}
