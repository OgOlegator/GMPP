using GMPP.MainApi.Models;
using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace GMPP.MainApi.Controllers
{
    /// <summary>
    /// Controller for work with entity User
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Object to work with data base
        /// </summary>
        private readonly IUserRepository _repository;
        private ResponseDto _response = new ResponseDto();

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get user info
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}")]
        public async Task<ResponseDto> GetUserInfo(string userId)
        {
            try
            {
                _response.Result = await _repository.GetUserInfo(int.Parse(userId));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Add new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseDto> CreateUser([FromBody] UserDto user)
        {
            try
            {
                _response.Result = await _repository.CreateUpdateUser(user);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Change info user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResponseDto> UpdateUser([FromBody] UserDto user)
        {
            try
            {
                _response.Result = await _repository.CreateUpdateUser(user);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{userId}")]
        public async Task<ResponseDto> DeleteUser(string userId)
        {
            try
            {
                _response.Result = await _repository.DeleteUser(int.Parse(userId));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

    }
}
