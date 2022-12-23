using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMPP.MainApi.Models
{
    /// <summary>
    /// Employee search status
    /// </summary>
    public enum StatusVacancy
    {
        Open,
        Close
    }

    public class Vacancy
    {
        /// <summary>
        /// Unique id vacancy
        /// </summary>
        public int Id { get; set; }

        [Required]
        public int IdProject { get; set; }

        /// <summary>
        /// Name vacancy
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        /// <summary>
        /// Description vacancy
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string Description { get; set; }

        /// <summary>
        /// Employee search status
        /// </summary>
        [Required]
        public StatusVacancy Status { get; set; }

        /// <summary>
        /// Navigation object
        /// </summary>
        [ForeignKey("IdProject")]
        public Project Project { get; set; }

    }
}
