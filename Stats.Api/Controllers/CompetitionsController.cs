using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stats.Api.Models;

namespace Stats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionsController : ControllerBase
    {
        private readonly StatsDbContext context;

        public CompetitionsController(StatsDbContext context)
        {
            this.context = context;
        }

        // GET: api/Competitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Competition>>> GetCompetitions()
        {
            return await context.Competitions.ToListAsync();
        }

        // GET: api/Competitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Competition>> GetCompetition(Guid id)
        {
            var competition = await context.Competitions.FindAsync(id);

            if (competition == null)
            {
                return NotFound();
            }

            return competition;
        }

        // PUT: api/Competitions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompetition(Guid id, Competition competition)
        {
            if (id != competition.Id)
            {
                return BadRequest();
            }

            context.Entry(competition).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitionExists(id))
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

        // POST: api/Competitions
        [HttpPost]
        public async Task<ActionResult<Competition>> PostCompetition(Competition competition)
        {
            context.Competitions.Add(competition);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCompetition", new { id = competition.Id }, competition);
        }

        // DELETE: api/Competitions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Competition>> DeleteCompetition(Guid id)
        {
            var competition = await context.Competitions.FindAsync(id);
            if (competition == null)
            {
                return NotFound();
            }

            context.Competitions.Remove(competition);
            await context.SaveChangesAsync();

            return competition;
        }

        private bool CompetitionExists(Guid id)
        {
            return context.Competitions.Any(e => e.Id == id);
        }
    }
}
