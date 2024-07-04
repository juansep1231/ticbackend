using AutoMapper;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.Models;

namespace backendfepon.ModelConfigurations.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.State_Name, opt => opt.MapFrom(src => src.State.State_Name))
                .ForMember(dest => dest.Category_Name, opt => opt.MapFrom(src => src.Category.Description))
                .ForMember(dest => dest.Provider_Name, opt => opt.MapFrom(src => src.Provider.Name));

            CreateMap<CreateUpdateProductDTO, Product>()
                .ForMember(dest => dest.State_Id, opt => opt.Ignore())
                .ForMember(dest => dest.Category_Id, opt => opt.Ignore())
                .ForMember(dest => dest.Provider_Id, opt => opt.Ignore());
        }
    }
}
