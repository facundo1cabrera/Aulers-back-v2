using AulersAPI.ApiModels;

namespace AulersAPI.Services.Interfaces
{
    public interface IMeasurementsService
    {
        Task CreateMeasurementsForUser(int userId);

        Task<MeasurementsDTO> GetUserMeasurements(int userId);

        Task<bool> UpdateMeasurementsForUser(MeasurementsDTO measurementsDTO, int userId);

    }
}
