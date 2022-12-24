using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GMPP.MainApi.Models.Dtos
{
    public class ProjectDto
    {

        /// <summary>
        /// Unique id project
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID of the user who created the project
        /// </summary>
        public int IdCreator { get; set; }

        /// <summary>
        /// Name project
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description project
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Create date project
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Project execution status
        /// </summary>
        public StatusProject Status { get; set; }

        /// <summary>
        /// Type project
        /// </summary>
        public TypeProject Type { get; set; }

        /// <summary>
        /// Desired skill level
        /// </summary>
        public LevelProject Level { get; set; }

    }
}
