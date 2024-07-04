using AutoMapper;
using backendfepon.DTOs.ContributorDTO;
using backendfepon.Models;

namespace backendfepon.ModelConfigurations.Profiles
{
    public class ContributorProfile : Profile
    {
        public ContributorProfile()
        {
            // Mapping from Contributor to ContributorDTO
            CreateMap<Contributor, ContributorDTO>()
                .ForMember(dest => dest.Plan_Name, opt => opt.MapFrom(src => src.ContributionPlan.Name))
                .ForMember(dest => dest.Plan_Economic_Value, opt => opt.MapFrom(src => src.ContributionPlan.Economic_Value))
                .ForMember(dest => dest.Student_FullName, opt => opt.MapFrom(src => src.Student.First_Name + ' ' + src.Student.Last_Name))
                .ForMember(dest => dest.Student_Faculty, opt => opt.MapFrom(src => src.Student.Faculty.Faculty_Name))
                .ForMember(dest => dest.Student_Career, opt => opt.MapFrom(src => src.Student.Career.Career_Name))
                .ForMember(dest => dest.Student_Email, opt => opt.MapFrom(src => src.Student.Email));

            // Mapping from CreateUpdateContributorDTO to Contributor
            CreateMap<CreateUpdateContributorDTO, Contributor>()
                .ForMember(dest => dest.Plan_Id, opt => opt.Ignore())
                .ForMember(dest => dest.Student_Id, opt => opt.Ignore());
        }
    }
}
