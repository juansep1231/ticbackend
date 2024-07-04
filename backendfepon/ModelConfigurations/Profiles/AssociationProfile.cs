using AutoMapper;
using backendfepon.DTOs.AssociationDTOs;
using backendfepon.Models;

namespace backendfepon.ModelConfigurations.Profiles
{
    public class AssociationProfile : Profile
    {
        public AssociationProfile()
        {
            CreateMap<Association, AssociationDTO>()
                .ForMember(dest => dest.State_Name, opt => opt.MapFrom(src => src.State.State_Name));

            CreateMap<CreateUpdateAssociationDTO, Association>()
                .ForMember(dest => dest.State_Id, opt => opt.Ignore());
        }
    }
}
