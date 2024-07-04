using AutoMapper;
using backendfepon.DTOs.EventDTOs;
using backendfepon.Models;

namespace backendfepon.ModelConfigurations.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            // Mapping from Event to EventDTO
            CreateMap<Event, EventDTO>()
                .ForMember(dest => dest.State_Name, opt => opt.MapFrom(src => src.State.State_Name));

            // Mapping from CreateUpdateEventDTO to Event
            CreateMap<CreateUpdateEventDTO, Event>()
                .ForMember(dest => dest.State_Id, opt => opt.Ignore());
        }
    }
}
