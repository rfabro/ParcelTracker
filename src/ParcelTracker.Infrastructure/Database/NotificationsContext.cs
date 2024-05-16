using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParcelTracker.Core.Notifications;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Infrastructure.Database;

public class NotificationsContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<NotificationEntity> Notifications { get; set; }

    public NotificationsContext()
    {
    }

    public NotificationsContext(DbContextOptions<NotificationsContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotificationEntity>().HasKey(table => table.NotificationId);
        modelBuilder.Entity<NotificationEntity>()
            .Property(table => table.DateCreated)
            .HasDefaultValue(DateTime.UtcNow);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("WebApiDatabase"));
        base.OnConfiguring(optionsBuilder);
    }
}