using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using webapi.Models;
using webapi.Dtos.Users;
using webapi.BaseControllers;
using webapi.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using webapi.Resources;

namespace webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = GetMappedUsersList(await _userService.GetAllUsers());

            return Ok(new { Items = users });
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            try
            {
                var user = GetMappedUser(await _userService.GetUserById(id));

                return Ok(new { Items = user });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="newUser">The user to create.</param>
        [HttpPost("Add")]
        public async Task<ActionResult> AddUser(Users newUser)
        {
            var users = GetMappedUser(await _userService.AddUser(newUser));

            return Ok(new { Items = users });
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user details.</param>
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, Users updatedUser)
        {
            try
            {
                var user = GetMappedUser(await _userService.UpdateUser(updatedUser));

                return Ok(new { Items = user });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                if(await _userService.DeleteUser(id))
                {
                return Ok(MessageKeys.DeletedSuccessfully);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private UsersDto GetMappedUser(Users user)
        {
            return _mapper.Map<UsersDto>(new UsersDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            });
        }

        private List<UsersDto> GetMappedUsersList(IEnumerable<Users> users)
        {
            List<Users> mappedList = users.Select(e =>
            new Users { Id = e.Id, Name = e.Name, Email = e.Email }).ToList();

            List<UsersDto> usersDtoList = mappedList.Select(c => _mapper.Map<UsersDto>(c)).ToList();
            return usersDtoList;
        }

    }
}