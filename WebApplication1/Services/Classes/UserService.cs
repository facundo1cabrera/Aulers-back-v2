using AulersAPI.ApiModels;
using AulersAPI.Infrastructure;
using AulersAPI.Infrastructure.Interfaces;
using AulersAPI.Models;
using AulersAPI.Services.Interfaces;
using AulersAPI.Utils;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersMicroservice.Clients;

namespace AulersAPI.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IConfiguration _config;
        private readonly IPublishEndpoint _publishEndpoint;

        public UserService(IUsersRepository usersRepository, IConfiguration config, IPublishEndpoint publishEndpoint)
        {
            _usersRepository = usersRepository;
            _config = config;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<AuthResponse> CreateUser(RegisterDTO registerDTO)
        {
            var userDB = await _usersRepository.GetUserByEmail(registerDTO.Email);
            if (userDB != null)
            {
                return null;
            }

            var user = new User()
            {
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password)
            };

            await _usersRepository.CreateUser(user);

            var userId = await _usersRepository.GetUserByEmail(registerDTO.Email);

            await _publishEndpoint.Publish(new UserCreated(userId.Id, registerDTO.Email, registerDTO.FirstName));

            return CreateToken(registerDTO.Email, false);
        }

        public async Task<AuthResponse> Login(LoginDTO loginDTO)
        {
            var userDB = await _usersRepository.GetUserByEmail(loginDTO.Email);

            if (userDB == null)
            {
                return null;
            }

            var samePassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, userDB.Password);

            if (samePassword)
            {
                return CreateToken(loginDTO.Email, userDB.IsAdmin);
            } else
            {
                return null;
            }
        }

        private AuthResponse CreateToken(string email, bool isAdmin)
        {
            var jwt = _config.GetSection("Jwt").Get<Jwt>();

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("email", email),
            };

            if (isAdmin)
            {
                claims.Add(new Claim("isAdmin", "1"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );

            return new AuthResponse()
            { 
                Token = new JwtSecurityTokenHandler().WriteToken(token), 
                Expiration = DateTime.Now.AddDays(1)
            };
        }

        public async Task<bool> ValidateUserExists(int userId)
        {
            var userDB = await _usersRepository.GetUserById(userId);

            if (userDB == null)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }
    }
}
