using AutoMapper;
using Stats.Api.Models;
using Stats.Common.Dto;

namespace Stats.Api.Business.Mappings
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameDto, Game>()
                .ForMember(x=>x.HomeTeamId,
                    o=>o.MapFrom(s=>s.HomeTeam))
                .ForMember(x => x.VisitorTeamId,
                    o => o.MapFrom(s => s.VisitorTeam))
                .ForMember(dest=>dest.RoundId, opt=>opt.MapFrom(src=>src.Round));

            CreateMap<Game, GameDto>()
                .ForMember(dest => dest.RoundNumber, opt => opt.Ignore())
                .ForMember(dest => dest.SeasonId, opt => opt.Ignore())
                ;
        }
    }
}
