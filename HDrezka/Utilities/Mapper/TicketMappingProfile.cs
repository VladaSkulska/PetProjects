using AutoMapper;
using HDrezka.Models;
using HDrezka.Models.DTOs;

namespace HDrezka.Utilities.Mapper
{
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile() 
        { 
            CreateMap<TicketDto, Ticket>();
            CreateMap<Ticket, TicketDto>();
        }
    }
}
