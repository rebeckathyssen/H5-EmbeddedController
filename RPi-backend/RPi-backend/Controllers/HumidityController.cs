using Microsoft.AspNetCore.Mvc;
using RPi_backend.Model;
using RPi_backend.Repository;

namespace RPi_backend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HumidityController : ControllerBase
    {
        private readonly IHumidityRepository humidityRepository;

        public HumidityController(IHumidityRepository humidityRepository)
        {
            this.humidityRepository = humidityRepository;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAllHumidities()
        {
            var res = await humidityRepository.GetAllHumidities();
            return res != null ? (IActionResult)Ok(res) : BadRequest();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetHumidity(int id)
        {
            try
            {
                return Ok(await humidityRepository.GetHumidity(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        // Endpoint to create a new product
        [HttpPost]
        public virtual async Task<IActionResult> PostHumidity(Humidity humidity)
        {
            try
            {
                return Ok(await humidityRepository.PostHumidity(humidity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        // Endpoint to update product
        [HttpPut]
        public virtual async Task<IActionResult> UpdateHumidity(Humidity humidity)
        {
            try
            {
                return Ok(await humidityRepository.UpdateHumidity(humidity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        // Endpoint to delete product
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteHumidity(int id)
        {
            try
            {
                return Ok(await humidityRepository.DeleteHumidity(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetLastHumidity()
        {
            try
            {
                return Ok(await humidityRepository.GetLastHumidity());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetLast50Humidities()
        {
            try
            {
                return Ok(await humidityRepository.GetLast50Humidities());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
