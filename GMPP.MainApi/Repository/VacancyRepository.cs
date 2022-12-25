using AutoMapper;
using GMPP.MainApi.DbContexts;
using GMPP.MainApi.Models;
using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GMPP.MainApi.Repository
{
    /// <summary>
    /// Class for work with the data base for the entity Vacancy.
    /// </summary>
    public class VacancyRepository : IVacancyRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public VacancyRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new vacancy or change vacancy info in data base
        /// </summary>
        /// <param name="vacancyDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<VacancyDto> CreateUpdateVacancy(VacancyDto vacancyDto)
        {
            var vacancy = _mapper.Map<Vacancy>(vacancyDto);

            if (vacancy.Id > 0)
            {
                _db.Vacancies.Update(vacancy);
            }
            else
            {
                _db.Vacancies.Add(vacancy);
            }

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create/update vacancy", ex);
            }

            return _mapper.Map<Vacancy, VacancyDto>(vacancy);
        }

        /// <summary>
        /// Delete vacancy in data base
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteVacancy(int id)
        {
            var vacancy = await _db.Vacancies.FirstOrDefaultAsync(item => item.Id == id);

            if (vacancy == null)
            {
                throw new ArgumentNullException(id.ToString(), "Vacancy not found");
            }

            _db.Vacancies.Remove(vacancy);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete vacancy in db", ex);
            }

            return true;
        }

        /// <summary>
        /// Get all vacancies in project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VacancyDto>> GetVacanciesByProject(int projectId)
        {
            var listVacancies = await _db.Vacancies.Where(item => item.IdProject == projectId).ToListAsync();

            return _mapper.Map<IEnumerable<VacancyDto>>(listVacancies);
        }

        /// <summary>
        /// Get concrete vacancy
        /// </summary>
        /// <param name="vacancyId"></param>
        /// <returns></returns>
        public async Task<VacancyDto> GetVacancyById(int vacancyId)
        {
            var vacancy = await _db.Vacancies.FirstOrDefaultAsync(item => item.Id == vacancyId);

            if (vacancy == null)
                throw new ArgumentNullException(vacancyId.ToString(), "Vacancy not found");

            return _mapper.Map<VacancyDto>(vacancy);
        }
    }
}
