using AutoMapper;
using HDrezka.Models.DTOs;
using HDrezka.Models;

namespace HDrezka.Utilities.Mapper
{
    public class ScheduleMappingProfile : Profile
    {
        public ScheduleMappingProfile()
        {
            CreateMap<ScheduleDto, Schedule>();
            CreateMap<Schedule, ScheduleDto>();
        }
    }
}
