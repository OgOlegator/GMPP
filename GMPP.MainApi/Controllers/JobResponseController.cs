using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Models.Enums;
using GMPP.MainApi.Repository.IRepository;
using GMPP.MainApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GMPP.MainApi.Controllers
{
    [Route("api/JobResponse")]
    [ApiController]
    public class JobResponseController : ControllerBase
    {
        private readonly IJobResponseRepository _jobResponseRepository;
        private readonly IApplyForJobService _applyForJobService;
        private ResponseDto _response = new ResponseDto();

        public JobResponseController(IJobResponseRepository jobPostingRepository, IApplyForJobService applyForJobService)
        {
            _jobResponseRepository = jobPostingRepository;
            _applyForJobService = applyForJobService;
        }

        /// <summary>
        /// Получение информации об отклике по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseDto> GetResponseById(string id)
        {
            try
            {
                _response.Result = await _jobResponseRepository.GetJobResponseById(id);
                _response.IsSuccess = true;
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
        public async Task<ResponseDto> GetResponsesByUser(string userId)
        {
            try
            {
                _response.Result = await _jobResponseRepository.GetJobResponsesByUser(userId);
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

        /// <summary>
        /// Получение откликов на вакансии в проекте
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("project/{projectId}")]
        public async Task<ResponseDto> GetResponsesByProject(string projectId)
        {
            try
            {
                _response.Result = await _jobResponseRepository.GetJobResponsesByProject(projectId);
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

        /// <summary>
        /// Получение откликов по ИД вакансии
        /// </summary>
        /// <param name="vacancyId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("vacancy/{vacancyId}")]
        public async Task<ResponseDto> GetResponsesByVacancy(string vacancyId)
        {
            try
            {
                _response.Result = await _jobResponseRepository.GetJobResponsesByVacancy(vacancyId);
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

        /// <summary>
        /// Отклик на вакансию
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ApplyForJob")]
        public async Task<ResponseDto> ApplyForJob(ApplyForJobDto applyForJob)
        {
            var newResponsdDto = new JobResponseDto {
                UserId = applyForJob.UserId,
                IdVacancy = applyForJob.VacancyId,
                TextResponsd = applyForJob.TextResponsd,
                State = StateJobResponse.ComingSoon,
            };

            try
            {
                var result = await _applyForJobService.SendResponse(applyForJob);

                if (!result)
                    return new ResponseDto { IsSuccess = false, DisplayMessage = "Не удалось отправить отклик"};

                _response.Result = await Task.Run(() 
                    => _jobResponseRepository.CreateJobResponse(newResponsdDto));

                _response.IsSuccess = true;
                _response.DisplayMessage = "Отклик отправлен";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                Results.Ok();
            }

            return _response;
        }

        /// <summary>
        /// Обновление статуса отклика
        /// </summary>
        /// <param name="idJobResponse"></param>
        /// <param name="newState"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ApplyForJob/{idJobResponse} {newState}")]
        public async Task<ResponseDto> UpdateJobResponse(string idJobResponse, StateJobResponse newState)
        {
            try
            {
                _response.Result = await _jobResponseRepository.UpdateJobResponse(idJobResponse, newState);
                _response.IsSuccess = true;
                _response.DisplayMessage = "Отклик обновлен";
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
        /// Удаление отклика
        /// </summary>
        /// <param name="idJobResponse"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("ApplyForJob/{idJobResponse}")]
        public async Task<ResponseDto> DeleteJobResponse(string idJobResponse)
        {
            try
            {
                _response.Result = await _jobResponseRepository.DeleteJobResponse(idJobResponse);
                _response.IsSuccess = true;
                _response.DisplayMessage = "Отклик удален";
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
