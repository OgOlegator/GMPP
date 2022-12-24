using AutoMapper;
using GMPP.MainApi.Models;
using GMPP.MainApi.Models.Dtos;

namespace GMPP.MainApi
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<UserDto, User>();
                config.CreateMap<User, UserDto>();
                config.CreateMap<ProjectDto, Project>();
                config.CreateMap<Project, ProjectDto>();
                config.CreateMap<VacancyDto, Vacancy>();
                config.CreateMap<Vacancy, VacancyDto>();
            });

            return mappingConfig;
        }

    }
}
