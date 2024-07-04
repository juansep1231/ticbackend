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
                .ForMember(dest => dest.Student_Name, opt => opt.MapFrom(src => src.Student.First_Name))
                .ForMember(dest => dest.Student_LastName, opt => opt.MapFrom(src => src.Student.Last_Name))
                .ForMember(dest => dest.Student_Birthday, opt => opt.MapFrom(src => src.Student.Birth_Date))
                .ForMember(dest => dest.Student_Phone, opt => opt.MapFrom(src => src.Student.Phone))
                .ForMember(dest => dest.Student_Faculty, opt => opt.MapFrom(src => src.Student.Faculty))
                .ForMember(dest => dest.Student_Career, opt => opt.MapFrom(src => src.Student.Career))
                .ForMember(dest => dest.Student_Semester, opt => opt.MapFrom(src => src.Student.Semester))
                .ForMember(dest => dest.Student_Email, opt => opt.MapFrom(src => src.Student.Email))
                .ForMember(dest => dest.Member_Role, opt => opt.MapFrom(src => src.Role.Role_Name));

            // Mapping from CreateUpdateAdministrativeMemberDTO to AdministrativeMember
            CreateMap<CreateUpdateAdministrativeMemberDTO, AdministrativeMember>()
                .ForMember(dest => dest.Student_Id, opt => opt.Ignore())
                .ForMember(dest => dest.Role_Id, opt => opt.Ignore());
        }
    }
}
