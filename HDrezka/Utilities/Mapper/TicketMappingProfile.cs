using AutoMapper;
using HDrezka.Models;
using HDrezka.Models.DTOs;

namespace HDrezka.Utilities.Mapper
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile() 
        { 
            CreateMap<TicketDto, Ticket>()
                .ForMember(dest => dest.SeatId, opt => opt.MapFrom(src => src.SeatNumber));
            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.SeatId));
        }
    }
}