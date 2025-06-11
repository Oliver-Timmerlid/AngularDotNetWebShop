using API.Data.Services;
using API.Dtos;
using API.Mappings;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("{id}/getCart")]
        public async Task<IActionResult> GetCart(int id)
        {
            var cart = await _userService.GetCartAsync(id);
            if (cart == null) return NotFound();
            return Ok(cart.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);
            var createdUser = await _userService.CreateAsync(userCreateDto);
            return CreatedAtAction(nameof(Register), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDto userDto)
        {
            var updated = await _userService.UpdateAsync(userDto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var user = await _userService.LoginAsync(loginDto);
            if (user == null) return Unauthorized(new { message = "Invalid credentials" });

            return Ok(user);
        }
    }
}
