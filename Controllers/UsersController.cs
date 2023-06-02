using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // In-memory storage for users
        private static List<Users> users = new List<Users>();

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetUsers()
        {
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        [HttpGet("{id}")]
        public ActionResult<Users> GetUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        [HttpPost]
        public ActionResult<Users> PostUser(Users user)
        {
            // Assign a unique ID to the user
            user.Id = users.Count + 1;

            // Add the user to the in-memory storage
            users.Add(user);

            return Ok(new { message = "User created" });
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user details.</param>
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, Users updatedUser)
        {
            // Find the user to update by ID
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            // Update the user
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            return Ok(new { message = "User updated" });         
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            // Find the user to delete by ID
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            // Remove the user from the in-memory storage
            users.Remove(user);

            return Ok(new {message = "User deleted"});

        }
    }
}
