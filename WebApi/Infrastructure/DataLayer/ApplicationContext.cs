using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; } = null!;
 
    public ApplicationContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new ConnectionString
        {
            DatabaseName = "team",
            Host = "localhost",
            Port = "5432",
            Password = "postgres",
            UserName = "postgres"
        };
        optionsBuilder.UseNpgsql(connectionString.Build());
    }
}