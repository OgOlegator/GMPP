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
        /// Add new vacancy in data base
        /// </summary>
        /// <param name="vacancyDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<VacancyDto> CreateVacancy(VacancyDto vacancyDto)
        {
            var vacancy = _mapper.Map<Vacancy>(vacancyDto);

            //Создание уникального ИД
            if (string.IsNullOrEmpty(vacancy.Id))
                vacancy.Id = Guid.NewGuid().ToString();

            _db.Vacancies.Add(vacancy);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create vacancy", ex);
            }

            return _mapper.Map<Vacancy, VacancyDto>(vacancy);
        }

        /// <summary>
        /// Change vacancy info in data base
        /// </summary>
        /// <param name="vacancyDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<VacancyDto> UpdateVacancy(VacancyDto vacancyDto)
        {
            var vacancy = _mapper.Map<Vacancy>(vacancyDto);

            var changeVacancy = await _db.Vacancies.FirstOrDefaultAsync(item => item.Id == vacancy.Id);

            if (changeVacancy == null)
                throw new ArgumentNullException($"Вакансия {vacancy.Name} не найдена");

            changeVacancy.Name = vacancy.Name;
            changeVacancy.Description = vacancy.Description;
            changeVacancy.Status = vacancyDto.Status;

            _db.Vacancies.Update(changeVacancy);
            
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update vacancy", ex);
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
        public async Task<bool> DeleteVacancy(string id)
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
        public async Task<IEnumerable<VacancyDto>> GetVacanciesByProject(string projectId)
        {
            var listVacancies = await _db.Vacancies.Where(item => item.IdProject == projectId).ToListAsync();

            return _mapper.Map<IEnumerable<VacancyDto>>(listVacancies);
        }

        /// <summary>
        /// Get concrete vacancy
        /// </summary>
        /// <param name="vacancyId"></param>
        /// <returns></returns>
        public async Task<VacancyDto> GetVacancyById(string vacancyId)
        {
            var vacancy = await _db.Vacancies.FirstOrDefaultAsync(item => item.Id == vacancyId);

            if (vacancy == null)
                throw new ArgumentNullException(vacancyId.ToString(), "Vacancy not found");

            return _mapper.Map<VacancyDto>(vacancy);
        }
    }
}
