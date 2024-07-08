using AutoMapper;
using backendfepon.DTOs.AdmnistrativeMemberDTOs;
using backendfepon.Models;
using System.Globalization;

namespace backendfepon.ModelConfigurations.Profiles
{
    public class AdministrativeMemberProfile : Profile
    {
        public AdministrativeMemberProfile()
        {
            // Mapping from AdministrativeMember to AdministrativeMemberDTO
            CreateMap<AdministrativeMember, AdministrativeMemberDTO>()
                .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.Last_Name))
                .ForMember(dest => dest.birthDate, opt => opt.MapFrom(src => src.Birth_Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.cellphone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.faculty, opt => opt.MapFrom(src => src.Faculty.Faculty_Name))
                .ForMember(dest => dest.career, opt => opt.MapFrom(src => src.Career.Career_Name))
                .ForMember(dest => dest.semester, opt => opt.MapFrom(src => src.Semester.Semester_Name))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.position, opt => opt.MapFrom(src => src.Role.Role_Name));

            // Mapping from CreateUpdateAdministrativeMemberDTO to AdministrativeMember
            CreateMap<CreateUpdateAdministrativeMemberDTO, AdministrativeMember>()
                //.ForMember(dest => dest, opt => opt.Ignore())
                .ForMember(dest => dest.Role_Id, opt => opt.Ignore());
        }
    }
}
