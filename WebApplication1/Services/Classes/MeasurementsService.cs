using AulersAPI.ApiModels;
using AulersAPI.Infrastructure.Interfaces;
using AulersAPI.Models;
using AulersAPI.Services.Interfaces;

namespace AulersAPI.Services.Classes
{
    public class MeasurementsService : IMeasurementsService
    {
        private readonly IMeasurementsRepository _measurementsRepository;
        private readonly IUsersRepository _userRepository;

        public MeasurementsService(IMeasurementsRepository measurementsRepository, IUsersRepository userRepository)
        {
            _measurementsRepository = measurementsRepository;
            _userRepository = userRepository;
        }

        public async Task CreateMeasurementsForUser(int userId)
        {
            await _measurementsRepository.InitUserMeasurements(userId);
        }

        public async Task<bool> UpdateMeasurementsForUser(MeasurementsDTO measurementsDTO, int userId)
        {
            var userDB = await _userRepository.GetUserById(userId);

            if (userDB == null)
            {
                return false;
            }

            var measurement = new Measurements()
            {
                UserId = userId,
                Gender = measurementsDTO.Gender,
                Chest = measurementsDTO.Chest,
                Hips = measurementsDTO.Hips,
                InsideLeg = measurementsDTO.InsideLeg,
                ShoulderWidth = measurementsDTO.ShoulderWidth,
                Sleeve = measurementsDTO.Sleeve,
                Waist = measurementsDTO.Waist,
                MinShoeSize = measurementsDTO.MinShoeSize,
                MaxShoeSize = measurementsDTO.MaxShoeSize
            };
        
            await _measurementsRepository.UpdateUserMeasurements(measurement);

            return true;
        }

        public async Task<MeasurementsDTO> GetUserMeasurements(int userId)
        {
            var userDB = await _userRepository.GetUserById(userId);

            if (userDB == null)
            {
                return null;
            }

            var measurements = await _measurementsRepository.GetUserMeasurements(userId);

            var measurementsDTO = new MeasurementsDTO()
            {
                Gender = measurements.Gender,
                Chest = measurements.Chest,
                Hips = measurements.Hips,
                InsideLeg = measurements.InsideLeg,
                ShoulderWidth = measurements.ShoulderWidth,
                Sleeve = measurements.Sleeve,
                Waist = measurements.Waist,
                MinShoeSize = measurements.MinShoeSize,
                MaxShoeSize = measurements.MaxShoeSize
            };

            return measurementsDTO;
        }
    }
}
