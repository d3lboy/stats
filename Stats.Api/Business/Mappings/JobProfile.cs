using AutoMapper;
using Stats.Api.Models;
using Stats.Common.Dto;

namespace Stats.Api.Business.Mappings
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<JobDto, Job>().ReverseMap();
        }
    }
}
