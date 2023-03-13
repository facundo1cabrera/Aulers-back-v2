using AulersAPI.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulersApiTest
{
    public class TestDbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _config;

        public TestDbConnectionFactory(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection GetConnection() => new SqlConnection(_config.GetConnectionString("testDbConnection"));
    }
}
