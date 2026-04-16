using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Model.Entities;

public class AppDbContext : DbContext
{
    public DbSet<QuantityHistoryRecord> QuantityHistory { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
