using AutoMapper;
using GMPP.MainApi.DbContexts;
using GMPP.MainApi.Models;
using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GMPP.MainApi.Repository
{
    public class JobResponseRepository : IJobResponseRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        private readonly IVacancyRepository _vacancyRepository;

        public JobResponseRepository(ApplicationDbContext db, IMapper mapper, IVacancyRepository vacancyRepository)
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
        public async Task<JobResponseDto> CreateJobResponse(JobResponseDto jobPostingDto)
        {
            var jobPosting = _mapper.Map<JobResponse>(jobPostingDto);

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

            return _mapper.Map<JobResponse, JobResponseDto>(jobPosting);
        }

        /// <summary>
        /// Удаление отклика
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteJobResponse(string id)
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
        public async Task<JobResponseDto> GetJobResponseById(string id)
        {
            var jobPosting = await _db.JobPostings.FirstOrDefaultAsync(item => item.Id == id);

            if (jobPosting == null)
                throw new ArgumentNullException(id.ToString(), "Job posting not found");

            return _mapper.Map<JobResponseDto>(jobPosting);
        }

        /// <summary>
        /// Получение вакансий по ИД проекта
        /// </summary>
        /// <param name="profectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<JobResponseDto>> GetJobResponsesByProject(string profectId)
        {
            var vacancies = await _vacancyRepository.GetVacanciesByProject(profectId);

            return await _db.JobPostings
                .AsQueryable()
                .Join(vacancies, 
                    jobPosting => jobPosting.IdVacancy, 
                    vacancy => vacancy.Id, 
                    (jobPosting, vacancy) => 
                        new JobResponseDto 
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
        public async Task<IEnumerable<JobResponseDto>> GetJobResponsesByUser(string userId)
        {
            var listJobPostings = await _db.JobPostings.Where(item => item.UserId == userId).ToListAsync();

            return _mapper.Map<List<JobResponseDto>>(listJobPostings);
        }

        /// <summary>
        /// Получение откликов по ИД вакансии
        /// </summary>
        /// <param name="vacancyId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<JobResponseDto>> GetJobResponsesByVacancy(string vacancyId)
        {
            var listJobPostings = await _db.JobPostings.Where(item => item.IdVacancy == vacancyId).ToListAsync();

            return _mapper.Map<List<JobResponseDto>>(listJobPostings);
        }

        /// <summary>
        /// Обновление информации Отклика в БД
        /// </summary>
        /// <param name="jobPostingDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<JobResponseDto> UpdateJobResponse(JobResponseDto jobPostingDto)
        {
            var jobPosting = _mapper.Map<JobResponse>(jobPostingDto);

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

            return _mapper.Map<JobResponseDto>(jobPosting);
        }
    }
}
