using AutoMapper;
using backendfepon.DTOs.InventoryMovementDTOs;
using backendfepon.Models;

namespace backendfepon.ModelConfigurations.Profiles
{
    public class InventoryMovementProfile : Profile
    {
        public InventoryMovementProfile()
        {
            // Mapping from InventoryMovement to InventoryMovementDTO
            CreateMap<InventoryMovement, InventoryMovementDTO>()
                .ForMember(dest => dest.Product_Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Inventory_Movement_Name, opt => opt.MapFrom(src => src.InventoryMovementType.Movement_Type_Name));

            // Mapping from CreateUpdateInventoryMovementDTO to InventoryMovement
            CreateMap<CreateUpdateInventoryMovementDTO, InventoryMovement>()
                .ForMember(dest => dest.Product_Id, opt => opt.Ignore())
                .ForMember(dest => dest.Inventory_Movement_Id, opt => opt.Ignore());
        }
    }
}
