using GMPP.MainApi.Models;
using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace GMPP.MainApi.Controllers
{
    [Route("api/responsd")]
    [ApiController]
    public class JobPostingController : ControllerBase
    {
        private readonly IJobPostingRepository _jobPostingRepository;
        private ResponseDto _response = new ResponseDto();

        public JobPostingController(IJobPostingRepository jobPostingRepository)
        {
            _jobPostingRepository = jobPostingRepository;
        }

        /// <summary>
        /// Получение информации об отклике по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseDto> GetResponsdsById(string id)
        {
            try
            {
                _response.Result = await _jobPostingRepository.GetJobPostingById(id);
            }
            catch (ArgumentNullException ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Получение откликов пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/{userId}")]
        public async Task<ResponseDto> GetResponsdsByUser(string userId)
        {
            try
            {
                _response.Result = await _jobPostingRepository.GetJobPostingsByUser(userId);
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
        /// Получение откликов на вакансии в проекте
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("project/{projectId}")]
        public async Task<ResponseDto> GetResponsdsByProject(string projectId)
        {
            try
            {
                _response.Result = await _jobPostingRepository.GetJobPostingsByProject(projectId);
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
        /// Отклик на вакансию
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseDto> ApplyForJob(ApplyForJobDto applyForJob)
        {
            var newResponsdDto = new JobPostingDto {
                UserId = applyForJob.UserId,
                IdVacancy = applyForJob.VacancyId,
                TextResponsd = applyForJob.TextResponsd,
                CreateDate = DateTime.Now,
                State = StateJobPosting.ComingSoon,
            };

            try
            {
                _response.Result = await Task.Run(() 
                    => _jobPostingRepository.CreateJobPosting(newResponsdDto));



                _response.IsSuccess = true;
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
