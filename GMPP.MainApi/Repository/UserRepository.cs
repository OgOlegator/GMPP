using AutoMapper;
using GMPP.MainApi.DbContexts;
using GMPP.MainApi.Models;
using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GMPP.MainApi.Repository
{
    /// <summary>
    /// Class for work with the data base for the entity User.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public UserRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new user or change user info in data base
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserDto> CreateUpdateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            if (user.Id > 0)
            {
                _db.Users.Update(user);
            }
            else
            {
                _db.Users.Add(user);
            }

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create/update user", ex);
            }

            return _mapper.Map<User, UserDto>(user);
        }

        /// <summary>
        /// Delete user and projects in data base 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(item => item.Id == id);

            if (user == null)
            {
                throw new ArgumentNullException("User not found");
            }

            _db.Users.Remove(user);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete user in db", ex);
            }

            return true;
        }

        /// <summary>
        /// Get info user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<UserDto> GetUserInfo(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(item => item.Id == id);

            if (user == null)
                throw new ArgumentNullException("id", "User not found");

            return _mapper.Map<UserDto>(user);
        }

    }
}
