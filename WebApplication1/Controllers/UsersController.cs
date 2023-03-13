using AulersAPI.ApiModels;
using AulersAPI.Infrastructure.Interfaces;
using AulersAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersRepository.GetUsers();
            return new OkObjectResult(users);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUser(RegisterDTO registerDTO)
        {
            var registerResponse = await _userService.CreateUser(registerDTO);
            if (registerResponse != null)
            {
                return Ok(registerResponse);
            } 
            else
            {
                return BadRequest("You already have an account");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
        {
            var loginResponse = await _userService.Login(loginDTO);

            if (loginResponse == null)
            {
                return NotFound();
            } 
            else
            {
                return Ok(loginResponse);
            }
        }


    }
}
