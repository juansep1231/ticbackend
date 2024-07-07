using AutoMapper;
using backendfepon.DTOs.AssociationDTOs;
using backendfepon.Models;

namespace backendfepon.ModelConfigurations.Profiles
{
    public class AssociationProfile : Profile
    {
        public AssociationProfile()
        {

            CreateMap<CreateUpdateAssociationDTO, Association>()
                .ForMember(dest => dest.State_Id, opt => opt.Ignore());

            CreateMap<Association, AssociationDTO>();
        }
    }
}
