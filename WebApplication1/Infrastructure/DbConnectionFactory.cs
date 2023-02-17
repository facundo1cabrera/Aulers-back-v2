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

        public IDbConnection GetConnection() => new SqlConnection(_config.GetConnectionString("defaultConnection"));
    }
}
