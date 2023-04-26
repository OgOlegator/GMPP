using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Models.Enums;

namespace GMPP.MainApi.Repository.IRepository
{
    public interface IJobResponseRepository
    {
        /// <summary>
        /// Создание отклика
        /// </summary>
        /// <param name="jobResponseDto"></param>
        /// <returns></returns>
        Task<JobResponseDto> CreateJobResponse(JobResponseDto jobResponseDto);

        /// <summary>
        /// Обновление отклика
        /// </summary>
        /// <param name="jobResponseDto"></param>
        /// <returns></returns>
        Task<JobResponseDto> UpdateJobResponse(string idJobResponse, StateJobPosting newState);

        /// <summary>
        /// Удаление отклика
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteJobResponse(string id);

        /// <summary>
        /// Получение отклика по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JobResponseDto> GetJobResponseById(string id);

        /// <summary>
        /// Получение откликов пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<JobResponseDto>> GetJobResponsesByUser(string userId);

        /// <summary>
        /// Получение откликов по проекту
        /// </summary>
        /// <param name="profectId"></param>
        /// <returns></returns>
        Task<IEnumerable<JobResponseDto>> GetJobResponsesByProject(string profectId);

        /// <summary>
        /// Получение откликов по вакансии
        /// </summary>
        /// <param name="vacancyId"></param>
        /// <returns></returns>
        Task<IEnumerable<JobResponseDto>> GetJobResponsesByVacancy(string vacancyId);
    }
}
