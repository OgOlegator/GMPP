using GMPP.MainApi.Models.Dtos;

namespace GMPP.MainApi.Repository.IRepository
{
    public interface IVacancyRepository
    {

        /// <summary>
        /// Add new vacancy or change vacancy info in data base
        /// </summary>
        /// <param name="vacancyDto"></param>
        /// <returns></returns>
        Task<VacancyDto> CreateUpdateVacancy(VacancyDto vacancyDto);

        /// <summary>
        /// Delete vacancy in data base
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteVacancy(int id);

        /// <summary>
        /// get all vacancies in project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<IEnumerable<VacancyDto>> GetVacanciesByProject(int projectId);

        /// <summary>
        /// Get concrete vacancy
        /// </summary>
        /// <param name="vacancyId"></param>
        /// <returns></returns>
        Task<VacancyDto> GetVacancyById(int vacancyId);

    }
}
