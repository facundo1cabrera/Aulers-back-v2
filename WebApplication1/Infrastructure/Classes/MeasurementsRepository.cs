using AulersAPI.Infrastructure.Interfaces;
using Dapper;

namespace AulersAPI.Infrastructure.Classes
{
    public class MeasurementsRepository : IMeasurementsRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public MeasurementsRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task InitUserMeasurements(int userId)
        {
            using var connection = _connectionFactory.GetConnection();
            var users = await connection.QueryAsync(@"
INSERT INTO [Measurements] 
    ([IdUser]) VALUES 
    (@userId)
",
            new
            {
                @userId = userId
            });
        }

    }
}
