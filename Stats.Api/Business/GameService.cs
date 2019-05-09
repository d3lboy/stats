using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stats.Api.Business.Exceptions;
using Stats.Api.Business.Interfaces;
using Stats.Api.Models;
using Stats.Common.Dto;

namespace Stats.Api.Business
{
    public class GameService : IGameService
    {
        private readonly StatsDbContext context;
        private readonly IMapper mapper;

        public GameService(StatsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GameDto> Get(Guid id)
        {
            var round = await context.Games.SingleOrDefaultAsync(x => x.Id == id);

            if (round == null)
            {
                throw new ItemNotFoundException();
            }

            return mapper.Map<GameDto>(round);
        }

        public async Task<Guid> Add(GameDto dto)
        {
            var game = await context.Games.SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (game != null)
            {
                throw new ItemAlreadyExistException($"Game {game.Id} not found!");
            }

            game = mapper.Map<Game>(dto);
            game.Id = Guid.NewGuid();
            context.Games.Add(game);

            await context.SaveChangesAsync();

            return game.Id;
        }

        public async Task<int> Add(List<GameDto> dtos)
        {
            var firstDto = dtos.FirstOrDefault();
            
            if (firstDto?.RoundNumber == null || !firstDto.SeasonId.HasValue)
                return default;

            try
            {
                var games = await (
                    from r in context.Rounds
                    join g in context.Games on r.Id equals g.RoundId
                    where 
                        r.SeasonId == firstDto.SeasonId.Value && r.RoundNumber == firstDto.RoundNumber.Value
                    select g).ToListAsync();

                var existing = mapper.Map<List<GameDto>>(games);

                dtos.ForEach(dto =>
                {
                    if (!existing.All(x => x.Round == dto.Round && x.RoundNumber == dto.RoundNumber)) return;

                    var game = mapper.Map<Game>(dto);
                    context.Games.Add(game);
                });

                return await context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return default;
            }
        }

        public async Task<int> Update(GameDto dto)
        {
            var game = await context.Games.SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (game == null)
            {
                throw new ItemNotFoundException();
            }

            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var game = await context.Games.SingleOrDefaultAsync(x => x.Id == id);
            if (game == null)
            {
                throw new ItemNotFoundException();
            }

            context.Games.Remove(game);
            return await context.SaveChangesAsync();
        }
    }
}
