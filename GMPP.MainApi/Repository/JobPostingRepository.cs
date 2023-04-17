using AutoMapper;
using GMPP.MainApi.DbContexts;
using GMPP.MainApi.Models;
using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GMPP.MainApi.Repository
{
    public class JobPostingRepository : IJobPostingRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        private readonly IVacancyRepository _vacancyRepository;

        public JobPostingRepository(ApplicationDbContext db, IMapper mapper, IVacancyRepository vacancyRepository)
        {
            _db = db;
            _mapper = mapper;
            _vacancyRepository = vacancyRepository;
        }

        /// <summary>
        /// Создание отклика
        /// </summary>
        /// <param name="jobPostingDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<JobPostingDto> CreateJobPosting(JobPostingDto jobPostingDto)
        {
            var jobPosting = _mapper.Map<JobPosting>(jobPostingDto);

            //Создание уникального ИД
            if (string.IsNullOrEmpty(jobPosting.Id))
                jobPosting.Id = Guid.NewGuid().ToString();

            _db.JobPostings.Add(jobPosting);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create job posting", ex);
            }

            return _mapper.Map<JobPosting, JobPostingDto>(jobPosting);
        }

        /// <summary>
        /// Удаление отклика
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteJobPosting(string id)
        {
            var jobPosting = await _db.JobPostings.FirstOrDefaultAsync(item => item.Id == id);

            if (jobPosting == null)
                throw new ArgumentNullException(id.ToString(), "Job posting not found");

            _db.JobPostings.Remove(jobPosting);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete job posting in db", ex);
            }

            return true;
        }

        /// <summary>
        /// Получение отклика по ИД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<JobPostingDto> GetJobPostingById(string id)
        {
            var jobPosting = await _db.JobPostings.FirstOrDefaultAsync(item => item.Id == id);

            if (jobPosting == null)
                throw new ArgumentNullException(id.ToString(), "Job posting not found");

            return _mapper.Map<JobPostingDto>(jobPosting);
        }

        /// <summary>
        /// Получение вакансий по ИД проекта
        /// </summary>
        /// <param name="profectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<JobPostingDto>> GetJobPostingsByProject(string profectId)
        {
            var vacancies = await _vacancyRepository.GetVacanciesByProject(profectId);

            return await _db.JobPostings
                .AsQueryable()
                .Join(vacancies, 
                    jobPosting => jobPosting.IdVacancy, 
                    vacancy => vacancy.Id, 
                    (jobPosting, vacancy) => 
                        new JobPostingDto 
                        { 
                            Id = jobPosting.Id, 
                            IdVacancy = jobPosting.IdVacancy, 
                            UserId = jobPosting.UserId,
                            CreateDate = jobPosting.CreateDate,
                            TextResponsd = jobPosting.TextResponsd,
                            State = jobPosting.State
                        })
                .OrderBy(x => x.CreateDate)
                .ToListAsync();
        }

        /// <summary>
        /// Получение откликов по ИД пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<JobPostingDto>> GetJobPostingsByUser(string userId)
        {
            var listJobPostings = await _db.JobPostings.Where(item => item.UserId == userId).ToListAsync();

            return _mapper.Map<List<JobPostingDto>>(listJobPostings);
        }

        /// <summary>
        /// Обновление информации Отклика в БД
        /// </summary>
        /// <param name="jobPostingDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<JobPostingDto> UpdateJobPosting(JobPostingDto jobPostingDto)
        {
            var jobPosting = _mapper.Map<JobPosting>(jobPostingDto);

            var changeJobPosting = await _db.JobPostings.FirstOrDefaultAsync(item => item.Id == jobPosting.Id);

            if (changeJobPosting == null)
                throw new ArgumentNullException("Отклик не найден");

            changeJobPosting.State = jobPosting.State;
            changeJobPosting.TextResponsd = jobPosting.TextResponsd;

            _db.JobPostings.Update(changeJobPosting);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update job posting", ex);
            }

            return _mapper.Map<JobPostingDto>(jobPosting);
        }
    }
}
