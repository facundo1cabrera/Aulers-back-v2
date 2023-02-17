using AulersAPI.ApiModels;

namespace AulersAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(RegisterDTO registerDTO);
    }
}
