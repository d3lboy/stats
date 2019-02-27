using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stats.Api.Models;
using Stats.Common.Dto;
using Stats.Common.Enums;

namespace Stats.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundsController : ControllerBase
    {
        private readonly StatsDbContext context;
        public RoundsController(StatsDbContext context)
        {
            this.context = context;
        }
        // GET: api/Rounds
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Rounds/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Rounds

        [HttpPost]
        public async Task<ActionResult<List<RoundDto>>> Post([FromBody]List<RoundDto> rounds)
        {
            string season = rounds.First()?.Season;
            var existingRounds = context.Rounds.Where(x => x.Season.Id == season).ToList();

            rounds.ForEach(round =>
            {
                if (existingRounds.All(x => x.Id != round.Id))
                {
                    context.Rounds.Add(new Round()
                    {
                        Id = round.Id,
                        SeasonId = season,
                        RoundType = RoundType.RegularSeason,
                        Timestamp = DateTime.Now
                    });
                }
            });
            await context.SaveChangesAsync();

            return rounds;
        }

        // PUT: api/Rounds/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
