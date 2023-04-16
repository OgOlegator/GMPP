using GMPP.MainApi.Models;
using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GMPP.MainApi.Controllers
{
    /// <summary>
    /// Controller for work with entity Project
    /// </summary>
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        /// <summary>
        /// Object to work with data base
        /// </summary>
        private readonly IProjectRepository _repository;
        private ResponseDto _response = new ResponseDto();

        public ProjectController(IProjectRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseDto> GetProjects()
        {
            try
            {
                _response.Result = await _repository.GetProjects();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Get concrete project 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{projectId:int}")]
        public async Task<ResponseDto> GetProjectById(string projectId)
        {
            try
            {
                _response.Result = await _repository.GetProjectById(projectId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Get User Projects
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByUser/{userId:int}")]
        public async Task<ResponseDto> GetProjectsByUser(string userId)
        {
            try
            {
                _response.Result = await _repository.GetProjectsByUser(userId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Add new project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseDto> CreateProject([FromBody] ProjectDto project)
        {
            try
            {
                _response.Result = await _repository.CreateUpdateProject(project);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Update project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResponseDto> UpdateProject([FromBody] ProjectDto project)
        {
            try
            {
                _response.Result = await _repository.CreateUpdateProject(project);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Delete project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{projectId:int}")]
        public async Task<ResponseDto> DeleteProject(string projectId)
        {
            try
            {
                _response.Result = await _repository.DeleteProject(projectId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

    }
}
