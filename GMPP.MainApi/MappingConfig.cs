using AutoMapper;
using GMPP.MainApi.Models;
using GMPP.MainApi.Models.Dtos;

namespace GMPP.MainApi
{
    /// <summary>
    /// Настройка маппера для сущностей
    /// </summary>
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProjectDto, Project>();
                config.CreateMap<Project, ProjectDto>();
                config.CreateMap<VacancyDto, Vacancy>();
                config.CreateMap<Vacancy, VacancyDto>();
                config.CreateMap<JobPostingDto, JobPosting>();
                config.CreateMap<JobPosting, JobPostingDto>();
            });

            return mappingConfig;
        }

    }
}
