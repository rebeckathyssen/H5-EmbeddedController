using Microsoft.AspNetCore.Mvc;
using RPi_backend.Model;
using RPi_backend.Repository;

namespace RPi_backend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TemperatureController : ControllerBase
    {
        private readonly ITemperatureRepository temperatureRepository; 

        public TemperatureController(ITemperatureRepository temperatureRepository)
        {
            this.temperatureRepository = temperatureRepository;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAllTemperatures()
        {
            var res = await temperatureRepository.GetAllTemperatures();
            return res != null ? (IActionResult)Ok(res) : BadRequest();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetTemp(int id)
        {
            try
            {
                return Ok(await temperatureRepository.GetTemp(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        // Endpoint to create a new product
        [HttpPost]
        public virtual async Task<IActionResult> PostTemp(Temperature temperature)
        {
            try
            {
                return Ok(await temperatureRepository.PostTemperature(temperature));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        // Endpoint to update product
        [HttpPut]
        public virtual async Task<IActionResult> UpdateTemperature(Temperature temperature)
        {
            try
            {
                return Ok(await temperatureRepository.UpdateTemperature(temperature));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        // Endpoint to delete product
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteTemperature(int id)
        {
            try
            {
                return Ok(await temperatureRepository.DeleteTemperature(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetLastTemperature()
        {
            try
            {
                return Ok(await temperatureRepository.GetLastTemp());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetLast50Temperatures()
        {
            try
            {
                return Ok(await temperatureRepository.GetLast50Temps());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

    }
}