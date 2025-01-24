using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; } = null!;

    public DbSet<EventEntity> Events { get; set; } = null!;

    public DbSet<RepeatEntity> Repeats { get; set; } = null!;
 
    public ApplicationContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new ConnectionString
        {
            DatabaseName = "calendar",
            Host = "localhost",
            Port = "5431",
            Password = "postgres",
            UserName = "postgres"
        };
        optionsBuilder.UseNpgsql(connectionString.Build());
    }   
}