using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Services.IServices;

namespace GMPP.MainApi.Services
{
    /// <summary>
    /// Сервис отправки отклика на вакансию на электронную почту
    /// </summary>
    public class ApplyForJobService : IApplyForJobService
    {
        public ApplyForJobService()
        {

        }

        /// <summary>
        /// Отправить отклик на Email
        /// </summary>
        /// <param name="applyForJob"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> SendResponsd(ApplyForJobDto applyForJob)
        {
            throw new NotImplementedException();
        }
    }
}
