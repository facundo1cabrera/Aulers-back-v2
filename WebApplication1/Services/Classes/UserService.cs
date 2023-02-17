using AulersAPI.ApiModels;
using AulersAPI.Infrastructure;
using AulersAPI.Models;
using AulersAPI.Services.Interfaces;

namespace AulersAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;

        public UserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<bool> CreateUser(RegisterDTO registerDTO)
        {
            var userDB = await _usersRepository.GetUser(registerDTO.Email);
            if (userDB != null)
            {
                return false;
            }

            var user = new User()
            {
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password)
            };

            await _usersRepository.CreateUser(user);
            return true;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {

        }
    }
}
