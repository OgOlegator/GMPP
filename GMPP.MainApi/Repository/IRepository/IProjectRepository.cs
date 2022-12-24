﻿using GMPP.MainApi.Models.Dtos;

namespace GMPP.MainApi.Repository.IRepository
{
    public interface IProjectRepository
    {

        /// <summary>
        /// Add new project or change project info in data base
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        Task<ProjectDto> CreateUpdateProject(ProjectDto projectDto);

        /// <summary>
        /// Delete project and tasks this project in data base
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteProject(int id);

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProjectDto>> GetProjects();

        /// <summary>
        /// Get concrete project in data base
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProjectDto> GetProjectById(int id);

        /// <summary>
        /// Get projects by user id in data base
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<ProjectDto>> GetProjectsByUser(int userId);

    }
}
