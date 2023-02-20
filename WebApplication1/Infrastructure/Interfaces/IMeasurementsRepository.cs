using AulersAPI.ApiModels;
using AulersAPI.Models;

namespace AulersAPI.Infrastructure.Interfaces
{
    public interface IMeasurementsRepository
    {
        Task InitUserMeasurements(int userId);

        Task UpdateUserMeasurements(Measurements measurements);

        Task<Measurements> GetUserMeasurements(int userId);
    }
}
