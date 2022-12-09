using Microsoft.EntityFrameworkCore;
using RPi_backend.Model;

namespace RPi_backend.Infrastructure
{
    public class SensorDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public SensorDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("DefaultDb");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Temperature> Temperatures { get; set; }

        public DbSet<Humidity> Humidities { get; set; }
    }

    
}
