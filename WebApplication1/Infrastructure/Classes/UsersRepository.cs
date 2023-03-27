using AulersAPI.Infrastructure.Interfaces;
using AulersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AulersAPI.Infrastructure.Classes
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _dbContext;

        public UsersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetUserByEmail(string userEmail)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == userEmail);

            return user;
        }

        public async Task<User> GetUserById(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            return user;
        }


        public async Task CreateUser(User user)
        {
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
