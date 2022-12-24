using System.ComponentModel.DataAnnotations;

namespace GMPP.MainApi.Models.Dtos
{
    public class UserDto
    {

        /// <summary>
        /// Unique Id user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name user
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// User CV
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// email address
        /// </summary>
        public string Email { get; set; }

    }
}
