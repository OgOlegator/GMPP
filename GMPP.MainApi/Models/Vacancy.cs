using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GMPP.MainApi.Models.Enums;

namespace GMPP.MainApi.Models
{

    public class Vacancy
    {
        /// <summary>
        /// Unique id vacancy
        /// </summary>
        public string Id { get; set; }

        [Required]
        public string IdProject { get; set; }

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
