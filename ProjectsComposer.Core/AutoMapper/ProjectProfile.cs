using AutoMapper;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.Core.AutoMapper;

public class ProjectProfile : Profile // TODO: IDK HOW TO USE YOU
{
    public ProjectProfile()
    {
        // Entity -> Domain Model
        CreateMap<ProjectEntity, Project>()
            .ConstructUsing(src => Project.Create(
                src.Id,
                src.Title,
                src.CustomerCompanyName,
                src.ContractorCompanyName,
                src.StartDate,
                src.EndDate).Value)
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));

        // Domain Model -> Entity
        CreateMap<Project, ProjectEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.CustomerCompanyName, opt => opt.MapFrom(src => src.CustomerCompanyName))
            .ForMember(dest => dest.ContractorCompanyName, opt => opt.MapFrom(src => src.ContractorCompanyName))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.LeaderId, opt => opt.Ignore());
    }
}