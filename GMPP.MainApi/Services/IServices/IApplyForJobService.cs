using GMPP.MainApi.Models.Dtos;

namespace GMPP.MainApi.Services.IServices
{
    /// <summary>
    /// Описание сервиса отправки отклика на вакансию на электронную почту
    /// </summary>
    public interface IApplyForJobService
    {

        /// <summary>
        /// Отправить отклик на Email
        /// </summary>
        /// <param name="applyForJob"></param>
        /// <returns></returns>
        Task<bool> SendResponsd(ApplyForJobDto applyForJob);

    }
}
