using onekarmaapi.Contracts; // For DTOs
using Microsoft.AspNetCore.Mvc;

namespace onekarmaapi.Controllers
{
    /// <summary>
    /// Controller for managing user entities.
    /// </summary>
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private readonly endpoint_Users _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UserController(endpoint_Users userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Adds a new user asynchronously.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost("user")]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDto user)
        {
            var response = await _userService.AddUser(user);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Gets all users asynchronously.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the list of users.</returns>
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers("SELECT * FROM c");
            return Ok(users);
        }

        /// <summary>
        /// Gets a user by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>An <see cref="IActionResult"/> containing the user.</returns>
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user.IsSuccess)
            {
                return Ok(user);
            }
            return NotFound(user);
        }

        /// <summary>
        /// Updates a user asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user data.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPut("user/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto user)
        {
            var response = await _userService.UpdateUser(id, user);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Deletes a user asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
