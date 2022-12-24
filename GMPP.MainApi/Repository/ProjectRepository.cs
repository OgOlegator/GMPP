using AutoMapper;
using GMPP.MainApi.DbContexts;
using GMPP.MainApi.Models;
using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GMPP.MainApi.Repository
{
    /// <summary>
    /// Class for work with the data base for the entity Project.
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProjectRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new project or change project info in data base
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ProjectDto> CreateUpdateProject(ProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);

            if (project.Id > 0)
            {
                _db.Projects.Update(project);
            }
            else
            {
                _db.Projects.Add(project);
            }

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create/update project", ex);
            }

            return _mapper.Map<Project, ProjectDto>(project);
        }

        /// <summary>
        /// Delete project and tasks this project in data base
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteProject(int id)
        {
            var project = await _db.Projects.FirstOrDefaultAsync(item => item.Id == id);

            if (project == null)
            {
                throw new ArgumentNullException("Project not found");
            }

            _db.Projects.Remove(project);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete project in db", ex);
            }

            return true;
        }

        /// <summary>
        /// Get concrete project in data base
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProjectDto> GetProjectById(int id)
        {
            var project = await _db.Projects.FirstOrDefaultAsync(item => item.Id == id);
            
            return _mapper.Map<ProjectDto>(project);
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectDto>> GetProjects()
        {
            var listProjects = await _db.Projects.ToListAsync();

            return _mapper.Map<List<ProjectDto>>(listProjects);
        }

        /// <summary>
        /// Get projects by user from data base
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectDto>> GetProjectsByUser(int userId)
        {
            var listProjects = await _db.Projects.FirstOrDefaultAsync(item => item.IdCreator == userId);
            
            return _mapper.Map<List<ProjectDto>>(listProjects);
        }
    }
}
