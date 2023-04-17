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
        /// Add new project in data base
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ProjectDto> CreateProject(ProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);

            if(string.IsNullOrEmpty(project.Id))
                //Создание уникального ИД
                project.Id = Guid.NewGuid().ToString();

            _db.Projects.Add(project);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create project", ex);
            }

            return _mapper.Map<Project, ProjectDto>(project);
        }

        /// <summary>
        /// Change project info in data base
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<ProjectDto> UpdateProject(ProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);

            var changeProject = await _db.Projects.FirstOrDefaultAsync(item => item.Id == project.Id);

            if (changeProject == null)
                throw new ArgumentNullException("Проект не найден");

            changeProject.Name = project.Name;
            changeProject.Description = project.Description;
            changeProject.Status = project.Status;
            changeProject.Level = project.Level;
            changeProject.Type = project.Type;
            changeProject.CreatedDate = project.CreatedDate;
            
            _db.Projects.Update(changeProject);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update project", ex);
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
        public async Task<bool> DeleteProject(string id)
        {
            var project = await _db.Projects.FirstOrDefaultAsync(item => item.Id == id);

            if (project == null)
            {
                throw new ArgumentNullException(id.ToString(), "Project not found");
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
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<ProjectDto> GetProjectById(string id)
        {
            var project = await _db.Projects.FirstOrDefaultAsync(item => item.Id == id);

            if (project == null)
                throw new ArgumentNullException(id.ToString(), "Project not found");

            return _mapper.Map<ProjectDto>(project);
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectDto>> GetProjects()
        {
            var listProjects = await _db.Projects.AsNoTracking().ToListAsync();

            return _mapper.Map<List<ProjectDto>>(listProjects);
        }

        /// <summary>
        /// Get projects by user from data base
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectDto>> GetProjectsByUser(string userId)
        {
            var listProjects = await _db.Projects.Where(item => item.IdCreator == userId).ToListAsync();

            return _mapper.Map<List<ProjectDto>>(listProjects);
        }
    }
}
