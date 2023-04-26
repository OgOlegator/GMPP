using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMPP.MainApi.Models
{
    /// <summary>
    /// Статусы отклика
    /// </summary>
    public enum StateJobPosting
    {
        /// <summary>
        /// Отклонен
        /// </summary>
        Rejected,

        /// <summary>
        /// Подтвержден
        /// </summary>
        Confirmed,

        /// <summary>
        /// Ожидает подтверждения
        /// </summary>
        ComingSoon 
    }

    /// <summary>
    /// Модель Отклика
    /// </summary>
    public class JobResponse
    {
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string IdVacancy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime LastChangedDate { get; set; } = DateTime.Now;

        public string TextResponsd { get; set; } = "";

        public StateJobPosting State { get; set; } = StateJobPosting.ComingSoon;

        /// <summary>
        /// Navigation object for Vacancies
        /// </summary>
        [ForeignKey("IdVacancy")]
        public Vacancy Vacancy { get; set; }

    }
}
