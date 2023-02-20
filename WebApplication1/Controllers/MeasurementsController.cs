using AulersAPI.ApiModels;
using AulersAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AulersAPI.Controllers
{
    [ApiController]
    [Route("api/measurements")]
    public class MeasurementsController: ControllerBase 
    {
        private readonly IMeasurementsService _measurementsService;

        public MeasurementsController(IMeasurementsService measurementsService)
        {
            _measurementsService = measurementsService;
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult<MeasurementsDTO>> GetUserMeasurements(int userId)
        {
            var measurements = await _measurementsService.GetUserMeasurements(userId);

            if (measurements == null)
            {
                return BadRequest("Invalid user");
            }
            else
            {
                return Ok(measurements);
            }
        }

        [HttpPut("{userId:int}")]
        public async Task<ActionResult> UpdateUserMeasurements(MeasurementsDTO measurements, int userId)
        {
            var result = await _measurementsService.UpdateMeasurementsForUser(measurements, userId);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Invalid user");
            }

        }

    }
}
