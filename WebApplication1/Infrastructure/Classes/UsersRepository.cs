using AulersAPI.Infrastructure.Interfaces;
using AulersAPI.Models;
using Dapper;

namespace AulersAPI.Infrastructure.Classes
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UsersRepository(IDbConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        public async Task<List<User>> GetUsers()
        {
            using var connection = _connectionFactory.GetConnection();
            var users = (await connection.QueryAsync<User>(@"
SELECT 
    U.Id,
    U.FirstName,
    U.LastName,
    U.Email
FROM
    [USERS] U")).ToList();

            return users;
        }

        public async Task<User> GetUserByEmail(string userEmail)
        {
            using var connection = _connectionFactory.GetConnection();
            var user = await connection.QueryFirstOrDefaultAsync<User>(@"
SELECT 
    U.Id,
    U.FirstName,
    U.LastName,
    U.Email,
    U.Password,
    U.IsAdmin
FROM
    [USERS] U
WHERE
    U.Email=@userEmail",
            new
            {
                @userEmail = userEmail
            });

            return user;
        }

        public async Task<User> GetUserById(int userId)
        {
            using var connection = _connectionFactory.GetConnection();
            var user = await connection.QueryFirstOrDefaultAsync<User>(@"
SELECT 
    U.Id,
    U.FirstName,
    U.LastName,
    U.Email,
    U.Password,
    U.IsAdmin
FROM
    [USERS] U
WHERE
    U.Id=@userId",
            new
            {
                @userId = userId
            });

            return user;
        }


        public async Task CreateUser(User user)
        {
            using var connection = _connectionFactory.GetConnection();
            await connection.QueryAsync(@"
INSERT INTO [Users]
(
    [FirstName], 
    [LastName],
    [Email], 
    [Password]
) 
VALUES (
    @firstname,
    @lastName,
    @email,
    @password
);",
            new
            {
                @firstname = user.FirstName,
                @lastname = user.LastName,
                @email = user.Email,
                @password = user.Password
            });
        }
    }
}
