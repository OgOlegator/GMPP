using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GMPP.MainApi.Models.Enums;

namespace GMPP.MainApi.Models
{

    public class Project
    {

        /// <summary>
        /// Unique id project
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ID of the user who created the project
        /// </summary>
        [Required]
        public string IdCreator { get; set; }

        /// <summary>
        /// Name project
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        /// <summary>
        /// Description project
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string Description { get; set; }

        /// <summary>
        /// Create date project
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Project execution status
        /// </summary>
        [Required]
        public StatusProject Status { get; set; }

        /// <summary>
        /// Type project
        /// </summary>
        [Required]
        public TypeProject Type { get; set; }

        /// <summary>
        /// Desired skill level
        /// </summary>
        [Required]
        public LevelProject Level { get; set; }

    }
}
