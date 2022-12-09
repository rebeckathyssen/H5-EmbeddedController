using Microsoft.EntityFrameworkCore;
using RPi_backend.Infrastructure;
using RPi_backend.Model;

namespace RPi_backend.Repository
{
    public class TemperatureRepository : ITemperatureRepository
    {
        private readonly SensorDbContext dbContext;

        public TemperatureRepository(SensorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Return all temperatures
        public virtual async Task<IEnumerable<Temperature>> GetAllTemperatures()
        {
            return await dbContext.Temperatures.ToListAsync();
        }

        // Return one temperature based on ID
        public virtual async Task<Temperature> GetTemp(int id)
        {
            return await dbContext.Temperatures.FindAsync(id);
        }

        // Update exisiting temperature
        public virtual async Task<Temperature> UpdateTemperature(Temperature temperature)
        {
            var entity = dbContext.Temperatures.Attach(temperature);
            dbContext.Update(temperature);
            entity.State = EntityState.Modified;
            dbContext.SaveChanges();
            return temperature;
        }

        // Post new temperature
        public virtual async Task<Temperature> PostTemperature(Temperature temperature)
        {
            await dbContext.AddAsync(temperature);
            dbContext.SaveChanges();
            return temperature;
        }

        // Delete temperature based on ID
        public virtual async Task<bool> DeleteTemperature(int id)
        {
            var entity = await GetTemp(id);

            if (entity == null) return false;

            var entityState = dbContext.Attach(entity);
            entityState.State = EntityState.Deleted;

            dbContext.Remove(entity);

            dbContext.SaveChanges();

            return true;
        }

        // Get last temperature
        public virtual async Task<Temperature> GetLastTemp()
        {
            return await dbContext.Temperatures.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<Temperature>> GetLast50Temps()
        {
            //return await dbContext.Temperatures.OrderByDescending(x => x.Id).Take(50).ToListAsync();
            return await dbContext.Temperatures.Skip(Math.Max(0, dbContext.Temperatures.Count() - 50)).ToListAsync();
        }
    }
}
