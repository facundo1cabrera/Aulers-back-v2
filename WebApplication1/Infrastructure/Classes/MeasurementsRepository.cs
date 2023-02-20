using AulersAPI.Infrastructure.Interfaces;
using AulersAPI.Models;
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
    ([UserId]) VALUES 
    (@userId)
",
            new
            {
                @userId = userId
            });
        }

        public async Task UpdateUserMeasurements(Measurements measurements)
        {
            using var connection = _connectionFactory.GetConnection();
            var users = await connection.QueryAsync(@"
UPDATE [Measurements]
SET
    [Gender]=@gender,
    [ShoulderWidth]=@shoulderWidth,
    [Chest]=@chest,
    [Waist]=@waist,
    [Sleeve]=@sleeve,
    [Hips]=@hips,
    [InsideLeg]=@insideLeg,
    [MinShoeSize]=@minShoeSize,
    [MaxShoeSize]=@maxShoeSize
WHERE
    [UserId]=@userId
",
            new
            {
                @gender = measurements.Gender,
                @shoulderWidth = measurements.ShoulderWidth,
                @chest = measurements.Chest,
                @waist = measurements.Waist,
                @sleeve = measurements.Sleeve,
                @hips = measurements.Hips,
                @insideLeg = measurements.InsideLeg,
                @minShoeSize = measurements.MinShoeSize,
                @maxShoeSize = measurements.MaxShoeSize,
                @userId = measurements.UserId
            });
        }

        public async Task<Measurements> GetUserMeasurements(int userId)
        {
            using var connection = _connectionFactory.GetConnection();
            var measurements = await connection.QueryFirstOrDefaultAsync<Measurements>(@"
SELECT
    [Gender],
    [ShoulderWidth],
    [Chest],
    [Waist],
    [Sleeve],
    [Hips],
    [InsideLeg],
    [MinShoeSize],
    [MaxShoeSize]
FROM
    [Measurements]
WHERE
    [UserId]=@userId
",
            new
            {
                @userId = userId
            });

            return measurements;
        }
    }
}
