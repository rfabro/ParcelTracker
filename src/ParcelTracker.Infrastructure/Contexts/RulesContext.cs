using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Infrastructure.Contexts;

public class RulesContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<RuleEntity> Rules { get; set; }

    public RulesContext()
    {
    }

    public RulesContext(DbContextOptions<RulesContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RuleEntity>().HasKey(table => table.RuleId);
        modelBuilder.Entity<RuleEntity>().ToTable("Rules");
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