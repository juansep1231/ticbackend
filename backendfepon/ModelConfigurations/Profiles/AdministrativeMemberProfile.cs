using AutoMapper;
using backendfepon.DTOs.AdmnistrativeMemberDTOs;
using backendfepon.Models;

namespace backendfepon.ModelConfigurations.Profiles
{
    public class AdministrativeMemberProfile : Profile
    {
        public AdministrativeMemberProfile()
        {
            // Mapping from AdministrativeMember to AdministrativeMemberDTO
            CreateMap<AdministrativeMember, AdministrativeMemberDTO>()
                .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.Student.First_Name))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.Student.Last_Name))
                .ForMember(dest => dest.birthDate, opt => opt.MapFrom(src => src.Student.Birth_Date))
                .ForMember(dest => dest.cellphone, opt => opt.MapFrom(src => src.Student.Phone))
                .ForMember(dest => dest.faculty, opt => opt.MapFrom(src => src.Student.Faculty))
                .ForMember(dest => dest.career, opt => opt.MapFrom(src => src.Student.Career))
                .ForMember(dest => dest.semester, opt => opt.MapFrom(src => src.Student.Semester))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Student.Email))
                .ForMember(dest => dest.position, opt => opt.MapFrom(src => src.Role.Role_Name));

            // Mapping from CreateUpdateAdministrativeMemberDTO to AdministrativeMember
            CreateMap<CreateUpdateAdministrativeMemberDTO, AdministrativeMember>()
                .ForMember(dest => dest.Student_Id, opt => opt.Ignore())
                .ForMember(dest => dest.Role_Id, opt => opt.Ignore());
        }
    }
}
