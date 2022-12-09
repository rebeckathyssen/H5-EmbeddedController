using Microsoft.EntityFrameworkCore;
using RPi_backend.Infrastructure;
using RPi_backend.Model;

namespace RPi_backend.Repository
{
    public class HumidityRepository : IHumidityRepository
    {
        private readonly SensorDbContext dbContext;

        public HumidityRepository(SensorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Return all temperatures
        public virtual async Task<IEnumerable<Humidity>> GetAllHumidities()
        {
            return await dbContext.Humidities.ToListAsync();
        }

        // Return one temperature based on ID
        public virtual async Task<Humidity> GetHumidity(int id)
        {
            return await dbContext.Humidities.FindAsync(id);
        }

        // Update exisiting temperature
        public virtual async Task<Humidity> UpdateHumidity(Humidity humidity)
        {
            var entity = dbContext.Humidities.Attach(humidity);
            dbContext.Update(humidity);
            entity.State = EntityState.Modified;
            dbContext.SaveChanges();
            return humidity;
        }

        // Post new temperature
        public virtual async Task<Humidity> PostHumidity(Humidity humidity)
        {
            await dbContext.AddAsync(humidity);
            dbContext.SaveChanges();
            return humidity;
        }

        // Delete temperature based on ID
        public virtual async Task<bool> DeleteHumidity(int id)
        {
            var entity = await GetHumidity(id);

            if (entity == null) return false;

            var entityState = dbContext.Attach(entity);
            entityState.State = EntityState.Deleted;

            dbContext.Remove(entity);

            dbContext.SaveChanges();

            return true;
        }

        // Get last temperature
        public virtual async Task<Humidity> GetLastHumidity()
        {
            return await dbContext.Humidities.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }

        // Get last 50
        public virtual async Task<IEnumerable<Humidity>> GetLast50Humidities()
        {
            return await dbContext.Humidities.Skip(Math.Max(0, dbContext.Temperatures.Count() - 50)).ToListAsync();
        }
    }
}
