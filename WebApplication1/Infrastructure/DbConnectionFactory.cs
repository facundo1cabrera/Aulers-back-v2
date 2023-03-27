using System.Data;
using System.Data.SqlClient;

namespace AulersAPI.Infrastructure
{
    public class DbConnectionFactory: IDbConnectionFactory
    {
        private readonly IConfiguration _config;

        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection GetConnection()
        {
            string serverName = Environment.GetEnvironmentVariable("DB_HOST");
            string port = Environment.GetEnvironmentVariable("DB_PORT");
            string dbName = Environment.GetEnvironmentVariable("DB_NAME");
            string username = Environment.GetEnvironmentVariable("DB_USERNAME");
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            if (string.IsNullOrEmpty(serverName) || string.IsNullOrEmpty(dbName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            string connectionString = $"postgres://{username}:{password}@{serverName}:{port}/{dbName}";
            return new SqlConnection(connectionString);
        }
    }
}
