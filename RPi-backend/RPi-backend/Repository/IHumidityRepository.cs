using RPi_backend.Model;

namespace RPi_backend.Repository
{
    public interface IHumidityRepository
    {
        public Task<IEnumerable<Humidity>> GetAllHumidities();

        public Task<Humidity> GetHumidity(int id);

        public Task<Humidity> UpdateHumidity(Humidity humidity);

        public Task<Humidity> PostHumidity(Humidity humidity);

        public Task<bool> DeleteHumidity(int id);

        public Task<Humidity> GetLastHumidity();

        public Task<IEnumerable<Humidity>> GetLast50Humidities();
    }
}
