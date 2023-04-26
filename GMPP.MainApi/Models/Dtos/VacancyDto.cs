using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GMPP.MainApi.Models.Enums;

namespace GMPP.MainApi.Models.Dtos
{
    public class VacancyDto
    {

        /// <summary>
        /// Unique id vacancy
        /// </summary>
        public string Id { get; set; }

        public string IdProject { get; set; }

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
