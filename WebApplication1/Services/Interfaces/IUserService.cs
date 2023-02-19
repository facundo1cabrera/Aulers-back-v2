using AulersAPI.ApiModels;

namespace AulersAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponse> CreateUser(RegisterDTO registerDTO);

        Task<AuthResponse> Login(LoginDTO loginDTO);
    }
}
