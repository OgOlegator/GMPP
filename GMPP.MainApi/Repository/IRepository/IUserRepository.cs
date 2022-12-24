using GMPP.MainApi.Models.Dtos;

namespace GMPP.MainApi.Repository.IRepository
{
    public interface IUserRepository
    {

        /// <summary>
        /// Add new user or change user info in data base
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        Task<UserDto> CreateUpdateUser(UserDto userDto);

        /// <summary>
        /// Delete user and projects in data base
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(int id);

        /// <summary>
        /// Get info user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDto> GetUserInfo(int id);

    }
}
