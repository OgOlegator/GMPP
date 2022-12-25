using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GMPP.MainApi.Controllers
{
    /// <summary>
    /// Controller for work with entity Vacancy
    /// </summary>
    [Route("api/vacancy")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        /// <summary>
        /// Object to work with data base
        /// </summary>
        private readonly IVacancyRepository _repository;
        private ResponseDto _response = new ResponseDto();

        public VacancyController(IVacancyRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get concrete Vacancy
        /// </summary>
        /// <param name="vacancyId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{vacancyId}")]
        public async Task<ResponseDto> GetVacancyById(string vacancyId)
        {
            try
            {
                _response.Result = await _repository.GetVacancyById(int.Parse(vacancyId));
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
        /// Get all vacancies by project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByProject/{projectId}")]
        public async Task<ResponseDto> GetVacanciesByProject(string projectId)
        {
            try
            {
                _response.Result = await _repository.GetVacanciesByProject(int.Parse(projectId));
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
        /// Add new vacancy
        /// </summary>
        /// <param name="vacancy"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseDto> CreateVacancy([FromBody] VacancyDto vacancy)
        {
            try
            {
                _response.Result = await _repository.CreateUpdateVacancy(vacancy);
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
        /// Update vacancy
        /// </summary>
        /// <param name="vacancy"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResponseDto> UpdateVacancy([FromBody] VacancyDto vacancy)
        {
            try
            {
                _response.Result = await _repository.CreateUpdateVacancy(vacancy);
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
        /// <param name="vacancyId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{vacancyId}")]
        public async Task<ResponseDto> DeleteVacancy(string vacancyId)
        {
            try
            {
                _response.Result = await _repository.DeleteVacancy(int.Parse(vacancyId));
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
