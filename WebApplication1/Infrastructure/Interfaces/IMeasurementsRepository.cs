namespace AulersAPI.Infrastructure.Interfaces
{
    public interface IMeasurementsRepository
    {
        Task InitUserMeasurements(int userId);
    }
}
