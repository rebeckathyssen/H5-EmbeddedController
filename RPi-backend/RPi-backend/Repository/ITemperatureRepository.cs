using RPi_backend.Model;

namespace RPi_backend.Repository
{
    public interface ITemperatureRepository
    {
        public Task<IEnumerable<Temperature>> GetAllTemperatures();

        public Task<Temperature> GetTemp(int id);

        public Task<Temperature> UpdateTemperature(Temperature temperature);

        public Task<Temperature> PostTemperature(Temperature temperature);

        public Task<bool> DeleteTemperature(int id);

        public Task<Temperature> GetLastTemp();

        public Task<IEnumerable<Temperature>> GetLast50Temps();
    }
}
