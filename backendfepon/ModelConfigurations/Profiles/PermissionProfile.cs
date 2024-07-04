using AutoMapper;
using backendfepon.DTOs.PermissionDTOs;
using backendfepon.Models;

namespace backendfepon.ModelConfigurations.Profiles
{
   
        public class PermissionProfile : Profile
        {
            public PermissionProfile()
            {
                // Mapping from Permission to PermissionDTO
                CreateMap<Permission, PermissionDTO>()
                    .ForMember(dest => dest.Event_Name, opt => opt.MapFrom(src => src.Event.Title));

                // Mapping from CreateUpdatePermissionDTO to Permission
                CreateMap<CreateUpdatePermissionDTO, Permission>()
                    .ForMember(dest => dest.Event_Id, opt => opt.Ignore());
            }
        }

}
