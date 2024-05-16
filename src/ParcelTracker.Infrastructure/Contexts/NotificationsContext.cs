using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Infrastructure.Contexts;

public class NotificationsContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<NotificationEntity> Notifications { get; set; }

    public NotificationsContext()
    {
    }

    public NotificationsContext(DbContextOptions<NotificationsContext> options) : base(options)
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
        modelBuilder.Entity<NotificationEntity>().ToTable("Notifications");
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=" + Environment.CurrentDirectory + "\\parceltracker.db", options =>
        {
            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });
        base.OnConfiguring(optionsBuilder);
    }
}