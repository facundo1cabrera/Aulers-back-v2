using AulersAPI.Models;

namespace AulersAPI.Infrastructure
{
    public interface IUsersRepository
    {
        Task<List<User>> GetUsers();

        Task CreateUser(User user);

        Task<User> GetUser(string userEmail);
    }
}
