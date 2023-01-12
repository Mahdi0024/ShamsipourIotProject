using Microsoft.EntityFrameworkCore;

namespace IotProject.Server.Data;

public class IotDbContext : DbContext
{
    public IotDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<TemperatureEntity> Temperatures { get; set; }
    public DbSet<HumidityEntity> Humidities { get; set; }
    public DbSet<TemperatureEntity> OutsideTemperatures { get; set; }
}