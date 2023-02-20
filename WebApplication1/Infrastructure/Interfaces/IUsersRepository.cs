using AulersAPI.Models;

namespace AulersAPI.Infrastructure.Interfaces
{
    public interface IUsersRepository
    {
        Task<List<User>> GetUsers();

        Task CreateUser(User user);

        Task<User> GetUserByEmail(string userEmail);

        Task<User> GetUserById(int userId);
    }
}
