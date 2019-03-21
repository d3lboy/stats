using AutoMapper;
using Stats.Api.Models;
using Stats.Common.Dto;

namespace Stats.Api.Business.Mappings
{
    public class RoundProfile : Profile
    {
        public RoundProfile()
        {
            CreateMap<RoundDto, Round>()
                .ForMember(x=>x.SeasonId,
                    o=>o.MapFrom(s=>s.Season)).ReverseMap();
        }
    }
}
