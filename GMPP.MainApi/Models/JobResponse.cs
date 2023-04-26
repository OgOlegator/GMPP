using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GMPP.MainApi.Models.Enums;

namespace GMPP.MainApi.Models
{
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

        public StateJobResponse State { get; set; } = StateJobResponse.ComingSoon;

        /// <summary>
        /// Navigation object for Vacancies
        /// </summary>
        [ForeignKey("IdVacancy")]
        public Vacancy Vacancy { get; set; }

    }
}
