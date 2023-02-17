using AulersAPI.ApiModels;
using AulersAPI.Infrastructure;
using AulersAPI.Models;
using AulersAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AulersAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController: ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUserService _userService;

        public UsersController(IUsersRepository usersRepository, IUserService userService)
        {
            _usersRepository = usersRepository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersRepository.GetUsers();
            return new OkObjectResult(users);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUser(RegisterDTO registerDTO)
        {
            var success = await _userService.CreateUser(registerDTO);
            if (success)
            {
                return Ok();
            } 
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
        {
            var user = await _usersRepository.GetUser(loginDTO.Email);
            if (user == null)
            {
                return BadRequest();
            }

            if (user.Password == loginDTO.Password)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
