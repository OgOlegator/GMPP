using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GMPP.MainApi.Models.Dtos
{
    public class VacancyDto
    {

        /// <summary>
        /// Unique id vacancy
        /// </summary>
        public int Id { get; set; }

        public int IdProject { get; set; }

        /// <summary>
        /// Name vacancy
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description vacancy
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Employee search status
        /// </summary>
        public StatusVacancy Status { get; set; }

    }
}
