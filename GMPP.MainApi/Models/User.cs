using System.ComponentModel.DataAnnotations;

namespace GMPP.MainApi.Models
{
    public class User
    {
        /// <summary>
        /// Unique Id user
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name user
        /// </summary>
        [Required]
        public string NickName { get; set; }

        /// <summary>
        /// User CV
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// email address
        /// </summary>
        [Required]
        public string email { get; set; }

    }
}
