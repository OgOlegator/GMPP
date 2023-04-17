using GMPP.MainApi.Models.Dtos;

namespace GMPP.MainApi.Repository.IRepository
{
    public interface IJobPostingRepository
    {
        /// <summary>
        /// Создание отклика
        /// </summary>
        /// <param name="jobPostingDto"></param>
        /// <returns></returns>
        Task<JobPostingDto> CreateJobPosting(JobPostingDto jobPostingDto);

        /// <summary>
        /// Обновление отклика
        /// </summary>
        /// <param name="jobPostingDto"></param>
        /// <returns></returns>
        Task<JobPostingDto> UpdateJobPosting(JobPostingDto jobPostingDto);

        /// <summary>
        /// Удаление отклика
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteJobPosting(string id);

        /// <summary>
        /// Получение отклика по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JobPostingDto> GetJobPostingById(string id);

        /// <summary>
        /// Получение откликов пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<JobPostingDto>> GetJobPostingsByUser(string userId);

        /// <summary>
        /// Получение откликов по проекту
        /// </summary>
        /// <param name="profectId"></param>
        /// <returns></returns>
        Task<IEnumerable<JobPostingDto>> GetJobPostingsByProject(string profectId);

    }
}
